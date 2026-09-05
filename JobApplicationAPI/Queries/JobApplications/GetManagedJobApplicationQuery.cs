using JobApplicationAPI.DTOs.JobApplications;
using MediatR;

namespace JobApplicationAPI.Queries.JobApplications
{
    public record GetManagedJobApplicationQuery(string? UserId, long JobApplicationId) : IRequest<JobApplicationEmployeeDto>;
}
