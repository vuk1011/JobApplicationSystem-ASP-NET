using Domain.Repositories;
using Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace JobApplicationAPI.Queries.JobApplications
{
    public class GetCandidateForJobApplicationHandler : IRequestHandler<GetCandidateForJobApplicationQuery, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly UserManager<AppUser> _userManager;

        public GetCandidateForJobApplicationHandler(IUnitOfWork uow, UserManager<AppUser> userManager)
        {
            _uow = uow;
            _userManager = userManager;
        }

        public async Task<Unit> Handle(GetCandidateForJobApplicationQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
