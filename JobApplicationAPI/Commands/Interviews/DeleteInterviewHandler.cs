using Domain.Repositories;
using JobApplicationAPI.Common.Exceptions;
using MediatR;

namespace JobApplicationAPI.Commands.Interviews
{
    public class DeleteInterviewHandler : IRequestHandler<DeleteInterviewCommand, Unit>
    {
        private readonly IUnitOfWork _uow;

        public DeleteInterviewHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Unit> Handle(DeleteInterviewCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserId))
                throw new BadRequestException("Couldn't resolve user");

            var employee = await _uow.Employees.GetByAppUserIdAsync(request.UserId);
            if (employee is null)
                throw new ResourceNotFoundException("Couldn't find employee");


            var interview = _uow.Interviews.GetByIdWithJobApplication(request.InterviewId);
            if (interview is null)
                throw new ResourceNotFoundException("Couldn't find interview");
            if (interview.JobApplication.EmployeeId != employee.Id)
            {
                throw new UnauthorizedException("Another employee is managing the associated job application for the interview");
            }
            if (interview.TimeScheduled < DateTime.Now)
                throw new ConflictException("Interview cannot be deleted after it took place");

            _uow.Interviews.Remove(interview);
            await _uow.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
