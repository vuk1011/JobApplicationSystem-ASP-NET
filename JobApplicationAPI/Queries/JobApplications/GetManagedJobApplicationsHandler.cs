using Domain.Repositories;
using JobApplicationAPI.Common.Exceptions;
using JobApplicationAPI.DTOs.JobApplications;
using JobApplicationAPI.Utilities;
using MediatR;

namespace JobApplicationAPI.Queries.JobApplications
{
    public class GetManagedJobApplicationsHandler : IRequestHandler<GetManagedJobApplicationsQuery, List<JobApplicationEmployeeDto>>
    {
        private readonly IUnitOfWork _uow;

        public GetManagedJobApplicationsHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<List<JobApplicationEmployeeDto>> Handle(GetManagedJobApplicationsQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserId))
                throw new BadRequestException("Couldn't resolve user");

            var employee = await _uow.Employees.GetByAppUserIdAsync(request.UserId);
            if (employee is null)
                throw new ResourceNotFoundException("Couldn't find employee");

            return _uow.JobApplications.GetManagedByEmployeeId(employee.Id)
                .Select(JobApplicationMapper.ToEmployeeDto)
                .ToList();
        }
    }
}
