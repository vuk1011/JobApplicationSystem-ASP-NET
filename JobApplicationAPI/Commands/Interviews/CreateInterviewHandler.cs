using Domain.Entities;
using Domain.Repositories;
using FluentValidation;
using JobApplicationAPI.Common.Exceptions;
using JobApplicationAPI.DTOs.Interviews;
using MediatR;

namespace JobApplicationAPI.Commands.Interviews
{
    public class CreateInterviewHandler : IRequestHandler<CreateInterviewCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly IValidator<CreateInterviewRequest> _validator;

        public CreateInterviewHandler(IUnitOfWork uow, IValidator<CreateInterviewRequest> validator)
        {
            _uow = uow;
            _validator = validator;
        }

        public async Task<Unit> Handle(CreateInterviewCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserId))
                throw new BadRequestException("Couldn't resolve user");

            var employee = await _uow.Employees.GetByAppUserIdAsync(request.UserId);
            if (employee is null)
                throw new ResourceNotFoundException("Couldn't find employee");

            var validationResult = await _validator.ValidateAsync(request.Request);
            if (!validationResult.IsValid)
                throw new BadRequestException(string.Join(" ", validationResult.Errors.Select(e => e.ErrorMessage)));

            var application = _uow.JobApplications.GetByIdWithDetails(request.Request.JobApplicationId);
            if (application is null)
            {
                throw new ResourceNotFoundException("Job application not found");
            }
            if (!application.IsManaged)
            {
                throw new ConflictException("This job application is not managed");
            }
            if (application.EmployeeId != employee.Id)
            {
                throw new UnauthorizedException("Another employee is managing this job application");
            }
            if (!JobApplicationStatusUtil.IsStatusChangeAllowed(application.Status, JobApplicationStatus.INTERVIEW_SCHEDULED))
            {
                throw new ConflictException("Interview cannot be scheduled from current status");
            }

            application.Status = JobApplicationStatus.INTERVIEW_SCHEDULED;
            _uow.JobApplications.Update(application);

            var interview = new Interview
            {
                Title = request.Request.Title,
                Description = request.Request.Description,
                TimeScheduled = request.Request.TimeScheduled,
                JobApplicationId = request.Request.JobApplicationId,
            };
            _uow.Interviews.Add(interview);
            await _uow.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
