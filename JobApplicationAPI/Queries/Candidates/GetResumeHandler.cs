using Domain.Repositories;
using JobApplicationAPI.Common.Exceptions;
using MediatR;

namespace JobApplicationAPI.Queries.Candidates
{
    public class GetResumeHandler : IRequestHandler<GetResumeQuery, byte[]>
    {
        private readonly IUnitOfWork _uow;

        public GetResumeHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<byte[]> Handle(GetResumeQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserId))
                throw new BadRequestException("Couldn't resolve user");

            var candidate = await _uow.Candidates.GetByAppUserIdAsync(request.UserId);
            if (candidate is null)
                throw new ResourceNotFoundException("Couldn't find candidate");
            if (candidate.Resume is null)
                throw new ResourceNotFoundException("Resume not found");

            return candidate.Resume;
        }
    }
}
