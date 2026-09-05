using Domain.Entities;
using JobApplicationAPI.DTOs.Interviews;

namespace JobApplicationAPI.Utilities
{
    public static class InterviewMapper
    {
        public static InterviewDto ToDto(Interview interview) => new()
        {
            Id = interview.Id,
            Title = interview.Title,
            Description = interview.Description,
            TimeScheduled = interview.TimeScheduled
        };
    }
}
