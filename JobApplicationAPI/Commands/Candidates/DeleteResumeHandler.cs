using Domain.Repositories;
using JobApplicationAPI.Common.Exceptions;
using MediatR;

namespace JobApplicationAPI.Commands.Candidates
{
    public class DeleteResumeHandler : IRequestHandler<DeleteResumeCommand, Unit>
    {
        private readonly IUnitOfWork _uow;

        public DeleteResumeHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Unit> Handle(DeleteResumeCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserId))
                throw new BadRequestException("Couldn't resolve user");

            var candidate = await _uow.Candidates.GetByAppUserIdAsync(request.UserId);
            if (candidate is null)
                throw new ResourceNotFoundException("Couldn't find candidate");

            candidate.Resume = null;
            _uow.Candidates.Update(candidate);
            await _uow.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
