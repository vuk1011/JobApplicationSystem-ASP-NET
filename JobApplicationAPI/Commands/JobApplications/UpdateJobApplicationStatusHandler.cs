using Domain.Entities;
using Domain.Repositories;
using FluentValidation;
using JobApplicationAPI.Common.Exceptions;
using JobApplicationAPI.DTOs.JobApplications;
using MediatR;

namespace JobApplicationAPI.Commands.JobApplications
{
    public class UpdateJobApplicationStatusHandler : IRequestHandler<UpdateJobApplicationStatusCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly IValidator<UpdateJobApplicationStatusRequest> _validator;

        public UpdateJobApplicationStatusHandler(IUnitOfWork uow, IValidator<UpdateJobApplicationStatusRequest> validator)
        {
            _uow = uow;
            _validator = validator;
        }

        public async Task<Unit> Handle(UpdateJobApplicationStatusCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserId))
                throw new BadRequestException("Couldn't resolve user");

            var employee = await _uow.Employees.GetByAppUserIdAsync(request.UserId);
            if (employee is null)
                throw new ResourceNotFoundException("Couldn't find employee");

            var validationResult = await _validator.ValidateAsync(request.Request);
            if (!validationResult.IsValid)
                throw new BadRequestException(string.Join(" ", validationResult.Errors.Select(e => e.ErrorMessage)));

            if (request.Request.Status == JobApplicationStatus.INTERVIEW_SCHEDULED)
                throw new ConflictException("Manually setting status to InterviewScheduled is not allowed");

            var application = _uow.JobApplications.GetByIdWithDetails(request.JobApplicationId);
            if (application is null)
                throw new ResourceNotFoundException("Job application not found");
            if (!application.IsManaged)
                throw new ConflictException("This job application is not managed");
            if (application.EmployeeId != employee.Id)
                throw new UnauthorizedException("Another employee is managing this job application");
            if (application.Status == JobApplicationStatus.ACCEPTED)
                throw new ConflictException("You cannot edit this application's status");
            if (!JobApplicationStatusUtil.IsStatusChangeAllowed(application.Status, request.Request.Status))
                throw new ConflictException("Status change not allowed");

            application.Status = request.Request.Status;
            _uow.JobApplications.Update(application);
            await _uow.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
