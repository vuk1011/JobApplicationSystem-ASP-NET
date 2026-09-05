using Domain.Entities;
using Domain.Repositories;
using JobApplicationAPI.Common.Exceptions;
using MediatR;

namespace JobApplicationAPI.Commands.JobApplications
{
    public class DeleteJobApplicationHandler : IRequestHandler<DeleteJobApplicationCommand, Unit>
    {
        private readonly IUnitOfWork _uow;

        public DeleteJobApplicationHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Unit> Handle(DeleteJobApplicationCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserId))
                throw new BadRequestException("Couldn't resolve user");

            var candidate = await _uow.Candidates.GetByAppUserIdAsync(request.UserId);
            if (candidate is null)
                throw new ResourceNotFoundException("Couldn't find candidate");

            var application = _uow.JobApplications.GetByIdForCandidate(request.JobApplicationId, candidate.Id);
            if (application is null)
                throw new ResourceNotFoundException("Job application not found");

            if (application.Status is JobApplicationStatus.OFFERED or JobApplicationStatus.ACCEPTED or JobApplicationStatus.REJECTED)
                throw new ConflictException("Job application cannot be withdrawn if state is Offered, Accepted or Rejected");

            _uow.JobApplications.Remove(application);
            await _uow.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
