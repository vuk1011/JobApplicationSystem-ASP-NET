using MediatR;

namespace JobApplicationAPI.Queries.Interviews
{
    public record GetInterviewsForEmployeeQuery : IRequest<Unit>;
}
