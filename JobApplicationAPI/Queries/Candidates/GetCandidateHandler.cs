using Domain.Repositories;
using Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace JobApplicationAPI.Queries.Candidates
{
    public class GetCandidateHandler : IRequestHandler<GetCandidateQuery, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly UserManager<AppUser> _userManager;

        public GetCandidateHandler(IUnitOfWork uow, UserManager<AppUser> userManager)
        {
            _uow = uow;
            _userManager = userManager;
        }

        public async Task<Unit> Handle(GetCandidateQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
