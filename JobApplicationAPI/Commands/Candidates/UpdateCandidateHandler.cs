using Domain.Repositories;
using FluentValidation;
using Infrastructure.Identity;
using JobApplicationAPI.Common.Exceptions;
using JobApplicationAPI.DTOs.Users;
using JobApplicationAPI.Utilities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace JobApplicationAPI.Commands.Candidates
{
    public class UpdateCandidateHandler : IRequestHandler<UpdateCandidateCommand, CandidateDto>
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

        public async Task<CandidateDto> Handle(UpdateCandidateCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserId))
                throw new BadRequestException("Couldn't resolve user");

            var candidate = await _uow.Candidates.GetByAppUserIdAsync(request.UserId);
            if (candidate is null)
                throw new ResourceNotFoundException("Couldn't find candidate");

            var appUser = await _userManager.FindByIdAsync(request.UserId);
            if (appUser is null)
                throw new BadRequestException("Couldn't resolve user");

            var validationResult = await _validator.ValidateAsync(request.Request);
            if (!validationResult.IsValid)
                throw new BadRequestException(string.Join(" ", validationResult.Errors.Select(e => e.ErrorMessage)));

            candidate.FirstName = request.Request.FirstName;
            candidate.LastName = request.Request.LastName;
            candidate.Sex = request.Request.Sex;
            candidate.Address = request.Request.Address;
            _uow.Candidates.Update(candidate);
            await _uow.SaveChangesAsync();

            appUser.PhoneNumber = request.Request.Phone;
            await _userManager.UpdateAsync(appUser);

            return CandidateMapper.ToDto(candidate, appUser);
        }
    }
}
