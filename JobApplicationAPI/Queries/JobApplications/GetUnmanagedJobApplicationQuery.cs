using JobApplicationAPI.DTOs.JobApplications;
using MediatR;

namespace JobApplicationAPI.Queries.JobApplications
{
    public record GetUnmanagedJobApplicationQuery(string? UserId, long JobApplicationId) : IRequest<JobApplicationEmployeeDto>;
}
