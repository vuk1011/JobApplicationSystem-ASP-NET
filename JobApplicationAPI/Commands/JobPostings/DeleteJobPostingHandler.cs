using Domain.Repositories;
using JobApplicationAPI.Common.Exceptions;
using MediatR;

namespace JobApplicationAPI.Commands.JobPostings
{
    public class DeleteJobPostingHandler : IRequestHandler<DeleteJobPostingCommand, Unit>
    {
        private readonly IUnitOfWork _uow;

        public DeleteJobPostingHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Unit> Handle(DeleteJobPostingCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserId))
                throw new BadRequestException("Couldn't resolve user");

            var employee = await _uow.Employees.GetByAppUserIdAsync(request.UserId);
            if (employee is null)
                throw new ResourceNotFoundException("Couldn't find employee");

            var jobPosting = _uow.JobPostings.GetByIdWithCompany(request.JobPostingId);
            if (jobPosting is null)
                throw new ResourceNotFoundException("Job posting not found");
            if (jobPosting.CompanyId != employee.CompanyId)
                throw new UnauthorizedException("This job posting isn't associated with your company");

            _uow.JobPostings.Remove(jobPosting);
            await _uow.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
