using MediatR;

namespace JobApplicationAPI.Queries.Interviews
{
    public class GetInterviewsForEmployeeHandler : IRequestHandler<GetInterviewsForEmployeeQuery, Unit>
    {
        public GetInterviewsForEmployeeHandler()
        {
            
        }

        public async Task<Unit> Handle(GetInterviewsForEmployeeQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
