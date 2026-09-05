using Domain.Repositories;
using JobApplicationAPI.Common.Exceptions;
using JobApplicationAPI.DTOs.JobApplications;
using JobApplicationAPI.Utilities;
using MediatR;

namespace JobApplicationAPI.Queries.JobApplications
{
    public class GetUnmanagedJobApplicationHandler : IRequestHandler<GetUnmanagedJobApplicationQuery, JobApplicationEmployeeDto>
    {
        private readonly IUnitOfWork _uow;

        public GetUnmanagedJobApplicationHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<JobApplicationEmployeeDto> Handle(GetUnmanagedJobApplicationQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserId))
                throw new BadRequestException("Couldn't resolve user");

            var employee = await _uow.Employees.GetByAppUserIdAsync(request.UserId);
            if (employee is null)
                throw new ResourceNotFoundException("Couldn't find employee");

            var application = _uow.JobApplications.GetByIdWithDetails(request.JobApplicationId);
            if (application is null)
                throw new ResourceNotFoundException("Job application not found");
            if (application.IsManaged)
                throw new ConflictException("This job application is already managed");

            return JobApplicationMapper.ToEmployeeDto(application);
        }
    }
}
