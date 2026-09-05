using Domain.Repositories;
using JobApplicationAPI.Common.Exceptions;
using JobApplicationAPI.DTOs.JobApplications;
using JobApplicationAPI.Utilities;
using MediatR;

namespace JobApplicationAPI.Queries.JobApplications
{
    public class GetJobApplicationForCandidateHandler : IRequestHandler<GetJobApplicationForCandidateQuery, JobApplicationCandidateDto>
    {
        private readonly IUnitOfWork _uow;

        public GetJobApplicationForCandidateHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<JobApplicationCandidateDto> Handle(GetJobApplicationForCandidateQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserId))
                throw new BadRequestException("Couldn't resolve user");

            var candidate = await _uow.Candidates.GetByAppUserIdAsync(request.UserId);
            if (candidate is null)
                throw new ResourceNotFoundException("Couldn't find candidate");

            var application = _uow.JobApplications.GetByIdForCandidate(request.JobApplicationId, candidate.Id);
            if (application is null)
                throw new ResourceNotFoundException("Job application not found");

            return JobApplicationMapper.ToCandidateDto(application);
        }
    }
}
