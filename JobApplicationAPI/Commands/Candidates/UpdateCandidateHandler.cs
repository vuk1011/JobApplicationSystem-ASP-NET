using Domain.Repositories;
using FluentValidation;
using Infrastructure.Identity;
using JobApplicationAPI.DTOs.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace JobApplicationAPI.Commands.Candidates
{
    public class UpdateCandidateHandler : IRequestHandler<UpdateCandidateCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly UserManager<AppUser> _userManager;
        private readonly IValidator<UpdateCandidateRequest> _validator;

        public UpdateCandidateHandler(IUnitOfWork uow, UserManager<AppUser> userManager, IValidator<UpdateCandidateRequest> validator)
        {
            _uow = uow;
            _userManager = userManager;
            _validator = validator;
        }

        public async Task<Unit> Handle(UpdateCandidateCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
