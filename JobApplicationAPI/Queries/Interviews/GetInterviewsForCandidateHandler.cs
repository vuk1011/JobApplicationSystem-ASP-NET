using Domain.Repositories;
using JobApplicationAPI.Common.Exceptions;
using JobApplicationAPI.DTOs.Interviews;
using JobApplicationAPI.Utilities;
using MediatR;

namespace JobApplicationAPI.Queries.Interviews
{
    public class GetInterviewsForCandidateHandler : IRequestHandler<GetInterviewsForCandidateQuery, List<InterviewDto>>
    {
        private readonly IUnitOfWork _uow;

        public GetInterviewsForCandidateHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<List<InterviewDto>> Handle(GetInterviewsForCandidateQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserId))
                throw new BadRequestException("Couldn't resolve user");

            var candidate = await _uow.Candidates.GetByAppUserIdAsync(request.UserId);
            if (candidate is null)
                throw new ResourceNotFoundException("Couldn't find candidate");

            var application = _uow.JobApplications.GetByIdWithDetails(request.JobApplicationId);
            if (application is null)
                throw new ResourceNotFoundException("Couldn't find job application");
            if (application.CandidateId != candidate.Id)
                throw new UnauthorizedException("You're unauthorized for this job application");

            return _uow.Interviews.GetByJobApplicationId(request.JobApplicationId)
                .Select(i => InterviewMapper.ToDto(i))
                .ToList();
        }
    }
}
