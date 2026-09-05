using Domain.Repositories;
using JobApplicationAPI.Common.Exceptions;
using JobApplicationAPI.DTOs.Offers;
using JobApplicationAPI.Utilities;
using MediatR;

namespace JobApplicationAPI.Queries.Offers
{
    public class GetOffersByJobApplicationForCandidateHandler : IRequestHandler<GetOffersByJobApplicationForCandidateQuery, List<OfferDto>>
    {
        private readonly IUnitOfWork _uow;

        public GetOffersByJobApplicationForCandidateHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<List<OfferDto>> Handle(GetOffersByJobApplicationForCandidateQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserId))
                throw new BadRequestException("Couldn't resolve user");

            var candidate = await _uow.Candidates.GetByAppUserIdAsync(request.UserId);
            if (candidate is null)
                throw new ResourceNotFoundException("Couldn't find candidate");

            var application = _uow.JobApplications.GetByIdWithDetails(request.JobApplicationId);
            if (application is null)
                throw new ResourceNotFoundException("Job application not found");
            if (application.CandidateId != candidate.Id)
                throw new UnauthorizedException("You're unauthorized for this job application");

            return _uow.Offers.GetByJobApplicationId(request.JobApplicationId).Select(OfferMapper.ToDto).ToList();
        }
    }
}
