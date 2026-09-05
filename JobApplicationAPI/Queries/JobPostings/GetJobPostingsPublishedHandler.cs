using Domain.Repositories;
using JobApplicationAPI.Common.Exceptions;
using JobApplicationAPI.DTOs.JobPostings;
using JobApplicationAPI.Utilities;
using MediatR;

namespace JobApplicationAPI.Queries.JobPostings
{
    public class GetJobPostingsPublishedHandler : IRequestHandler<GetJobPostingsPublishedQuery, List<JobPostingDto>>
    {
        private readonly IUnitOfWork _uow;

        public GetJobPostingsPublishedHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<List<JobPostingDto>> Handle(GetJobPostingsPublishedQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserId))
                throw new BadRequestException("Couldn't resolve user");

            var candidate = await _uow.Candidates.GetByAppUserIdAsync(request.UserId);
            if (candidate is null)
                throw new ResourceNotFoundException("Couldn't find candidate");

            return _uow.JobPostings.GetAllPublished().Select(JobPostingMapper.ToDto).ToList();
        }
    }
}
