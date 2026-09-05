using JobApplicationAPI.DTOs.Interviews;
using MediatR;

namespace JobApplicationAPI.Queries.Interviews
{
    public record GetInterviewsForEmployeeQuery(string? UserId, long JobApplicationId) : IRequest<List<InterviewDto>>;
}
