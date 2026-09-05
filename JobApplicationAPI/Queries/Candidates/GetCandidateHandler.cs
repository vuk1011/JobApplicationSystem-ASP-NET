using Domain.Repositories;
using Infrastructure.Identity;
using JobApplicationAPI.Common.Exceptions;
using JobApplicationAPI.DTOs.Users;
using JobApplicationAPI.Utilities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace JobApplicationAPI.Queries.Candidates
{
    public class GetCandidateHandler : IRequestHandler<GetCandidateQuery, CandidateDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly UserManager<AppUser> _userManager;

        public GetCandidateHandler(IUnitOfWork uow, UserManager<AppUser> userManager)
        {
            _uow = uow;
            _userManager = userManager;
        }

        public async Task<CandidateDto> Handle(GetCandidateQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserId))
                throw new BadRequestException("Couldn't resolve user");

            var candidate = await _uow.Candidates.GetByAppUserIdAsync(request.UserId);
            if (candidate is null)
                throw new ResourceNotFoundException("Couldn't find candidate");

            var appUser = await _userManager.FindByIdAsync(request.UserId);
            if (appUser is null)
                throw new BadRequestException("Couldn't resolve user");

            return CandidateMapper.ToDto(candidate, appUser);
        }
    }
}
