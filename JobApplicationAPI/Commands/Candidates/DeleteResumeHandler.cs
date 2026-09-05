using Domain.Repositories;
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
            throw new NotImplementedException();
        }
    }
}
