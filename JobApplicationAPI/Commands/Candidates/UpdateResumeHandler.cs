using Domain.Repositories;
using JobApplicationAPI.Common.Exceptions;
using MediatR;

namespace JobApplicationAPI.Commands.Candidates
{
    public class UpdateResumeHandler : IRequestHandler<UpdateResumeCommand, Unit>
    {
        private readonly IUnitOfWork _uow;

        public UpdateResumeHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Unit> Handle(UpdateResumeCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserId))
                throw new BadRequestException("Couldn't resolve user");

            var candidate = await _uow.Candidates.GetByAppUserIdAsync(request.UserId);
            if (candidate is null)
                throw new ResourceNotFoundException("Couldn't find candidate");

            using var memoryStream = new MemoryStream();
            await request.FileStream.CopyToAsync(memoryStream, cancellationToken);
            candidate.Resume = memoryStream.ToArray();

            _uow.Candidates.Update(candidate);
            await _uow.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
