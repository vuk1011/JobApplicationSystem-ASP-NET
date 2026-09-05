using Domain.Repositories;
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
            throw new NotImplementedException();
        }
    }
}
