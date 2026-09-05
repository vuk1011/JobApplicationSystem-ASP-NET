using Domain.Repositories;
using JobApplicationAPI.Common.Exceptions;
using JobApplicationAPI.DTOs.JobPostings;
using JobApplicationAPI.Utilities;
using MediatR;

namespace JobApplicationAPI.Queries.JobPostings
{
    public class GetJobPostingForEmployeeHandler : IRequestHandler<GetJobPostingForEmployeeQuery, JobPostingDto>
    {
        private readonly IUnitOfWork _uow;

        public GetJobPostingForEmployeeHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<JobPostingDto> Handle(GetJobPostingForEmployeeQuery request, CancellationToken cancellationToken)
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

            return JobPostingMapper.ToDto(jobPosting);
        }
    }
}
