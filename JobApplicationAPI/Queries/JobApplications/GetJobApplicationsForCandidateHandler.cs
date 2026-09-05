using Domain.Repositories;
using JobApplicationAPI.Common.Exceptions;
using JobApplicationAPI.DTOs.JobApplications;
using JobApplicationAPI.Utilities;
using MediatR;

namespace JobApplicationAPI.Queries.JobApplications
{
    public class GetJobApplicationsForCandidateHandler : IRequestHandler<GetJobApplicationsForCandidateQuery, List<JobApplicationCandidateDto>>
    {
        private readonly IUnitOfWork _uow;

        public GetJobApplicationsForCandidateHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<List<JobApplicationCandidateDto>> Handle(GetJobApplicationsForCandidateQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserId))
                throw new BadRequestException("Couldn't resolve user");

            var candidate = await _uow.Candidates.GetByAppUserIdAsync(request.UserId);
            if (candidate is null)
                throw new ResourceNotFoundException("Couldn't find candidate");

            return _uow.JobApplications.GetByCandidateId(candidate.Id).Select(JobApplicationMapper.ToCandidateDto).ToList();
        }
    }
}
