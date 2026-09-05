using Domain.Repositories;
using MediatR;

namespace JobApplicationAPI.Queries.Candidates
{
    public class GetResumeHandler : IRequestHandler<GetResumeQuery, Unit>
    {
        private readonly IUnitOfWork _uow;

        public GetResumeHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Unit> Handle(GetResumeQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
