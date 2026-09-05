using Domain.Repositories;
using JobApplicationAPI.Common.Exceptions;
using JobApplicationAPI.DTOs.JobApplications;
using JobApplicationAPI.Utilities;
using MediatR;

namespace JobApplicationAPI.Queries.JobApplications
{
    public class GetUnmanagedJobApplicationsByJobPostingHandler : IRequestHandler<GetUnmanagedJobApplicationsByJobPostingQuery, List<JobApplicationEmployeeDto>>
    {
        private readonly IUnitOfWork _uow;

        public GetUnmanagedJobApplicationsByJobPostingHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<List<JobApplicationEmployeeDto>> Handle(GetUnmanagedJobApplicationsByJobPostingQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserId))
                throw new BadRequestException("Couldn't resolve user");

            var employee = await _uow.Employees.GetByAppUserIdAsync(request.UserId);
            if (employee is null)
                throw new ResourceNotFoundException("Couldn't find employee");

            var jobPosting = _uow.JobPostings.GetByIdWithCompany(request.JobPostingId);
            if (jobPosting is null)
                throw new ResourceNotFoundException("Job posting not found");

            return _uow.JobApplications.GetUnmanagedByJobPostingId(request.JobPostingId)
                .Select(JobApplicationMapper.ToEmployeeDto)
                .ToList();
        }
    }
}
