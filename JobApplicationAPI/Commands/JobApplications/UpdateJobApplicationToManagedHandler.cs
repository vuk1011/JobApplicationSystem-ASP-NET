using Domain.Entities;
using Domain.Repositories;
using FluentValidation;
using JobApplicationAPI.Common.Exceptions;
using JobApplicationAPI.DTOs.JobApplications;
using MediatR;

namespace JobApplicationAPI.Commands.JobApplications
{
    public class UpdateJobApplicationToManagedHandler : IRequestHandler<UpdateJobApplicationToManagedCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly IValidator<ManageJobApplicationRequest> _validator;

        public UpdateJobApplicationToManagedHandler(IUnitOfWork uow, IValidator<ManageJobApplicationRequest> validator)
        {
            _uow = uow;
            _validator = validator;
        }

        public async Task<Unit> Handle(UpdateJobApplicationToManagedCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserId))
                throw new BadRequestException("Couldn't resolve user");

            var employee = await _uow.Employees.GetByAppUserIdAsync(request.UserId);
            if (employee is null)
                throw new ResourceNotFoundException("Couldn't find employee");



            var validationResult = await _validator.ValidateAsync(request.Request);
            if (!validationResult.IsValid)
                throw new BadRequestException(string.Join(" ", validationResult.Errors.Select(e => e.ErrorMessage)));

            var application = _uow.JobApplications.GetByIdWithDetails(request.Request.ApplicationId);
            if (application is null)
                throw new ResourceNotFoundException("Job application not found");
            if (application.IsManaged)
                throw new ConflictException("This job application is already managed");

            application.EmployeeId = employee.Id;
            application.Status = JobApplicationStatus.UNDER_REVIEW;
            _uow.JobApplications.Update(application);
            await _uow.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
