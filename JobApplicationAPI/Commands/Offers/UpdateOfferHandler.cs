using Domain.Entities;
using Domain.Repositories;
using FluentValidation;
using JobApplicationAPI.Common.Exceptions;
using JobApplicationAPI.DTOs.Offers;
using MediatR;

namespace JobApplicationAPI.Commands.Offers
{
    public class UpdateOfferHandler : IRequestHandler<UpdateOfferCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly IValidator<UpdateOfferRequest> _validator;

        public UpdateOfferHandler(IUnitOfWork uow, IValidator<UpdateOfferRequest> validator)
        {
            _uow = uow;
            _validator = validator;
        }

        public async Task<Unit> Handle(UpdateOfferCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserId))
                throw new BadRequestException("Couldn't resolve user");

            var candidate = await _uow.Candidates.GetByAppUserIdAsync(request.UserId);
            if (candidate is null)
                throw new ResourceNotFoundException("Couldn't find candidate");

            var validationResult = await _validator.ValidateAsync(request.Request);
            if (!validationResult.IsValid)
                throw new BadRequestException(string.Join(" ", validationResult.Errors.Select(e => e.ErrorMessage)));

            var offer = _uow.Offers.GetByIdWithJobApplication(request.OfferId);
            if (offer is null)
                throw new ResourceNotFoundException("Offer not found");

            var application = offer.JobApplication;
            if (application.CandidateId != candidate.Id)
                throw new UnauthorizedException("You're unauthorized for this job application");
            if (application.Status == JobApplicationStatus.ACCEPTED)
                throw new ConflictException("Offer cannot be updated in application's final status");
            if (offer.Accepted is not null)
                throw new ConflictException("Offer cannot be updated after it got accepted or rejected");

            var targetStatus = request.Request.Accepted ? JobApplicationStatus.ACCEPTED : JobApplicationStatus.REJECTED;
            if (!JobApplicationStatusUtil.IsStatusChangeAllowed(application.Status, targetStatus))
                throw new ConflictException("Offer cannot be updated when job application is in current status");

            offer.Accepted = request.Request.Accepted;
            application.Status = targetStatus;
            _uow.Offers.Update(offer);
            _uow.JobApplications.Update(application);
            await _uow.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
