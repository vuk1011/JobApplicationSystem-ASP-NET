using Domain.Repositories;
using MediatR;

namespace JobApplicationAPI.Queries.Interviews
{
    public class GetInterviewsForCandidateHandler : IRequestHandler<GetInterviewsForCandidateQuery, Unit>
    {
        private readonly IUnitOfWork _uow;

        public GetInterviewsForCandidateHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Unit> Handle(GetInterviewsForCandidateQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
