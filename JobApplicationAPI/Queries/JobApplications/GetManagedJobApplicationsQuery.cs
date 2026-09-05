using JobApplicationAPI.DTOs.JobApplications;
using MediatR;

namespace JobApplicationAPI.Queries.JobApplications
{
    public record GetManagedJobApplicationsQuery(string? UserId) : IRequest<List<JobApplicationEmployeeDto>>;
}
