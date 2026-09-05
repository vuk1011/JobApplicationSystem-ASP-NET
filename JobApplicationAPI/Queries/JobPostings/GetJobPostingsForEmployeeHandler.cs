using Domain.Repositories;
using JobApplicationAPI.Common.Exceptions;
using JobApplicationAPI.DTOs.JobPostings;
using JobApplicationAPI.Utilities;
using MediatR;

namespace JobApplicationAPI.Queries.JobPostings
{
    public class GetJobPostingsForEmployeeHandler : IRequestHandler<GetJobPostingsForEmployeeQuery, List<JobPostingDto>>
    {
        private readonly IUnitOfWork _uow;

        public GetJobPostingsForEmployeeHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<List<JobPostingDto>> Handle(GetJobPostingsForEmployeeQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserId))
                throw new BadRequestException("Couldn't resolve user");

            var employee = await _uow.Employees.GetByAppUserIdAsync(request.UserId);
            if (employee is null)
                throw new ResourceNotFoundException("Couldn't find employee");

            return _uow.JobPostings.GetAllByCompanyId(employee.CompanyId)
                .Select(JobPostingMapper.ToDto)
                .ToList();
        }
    }
}
