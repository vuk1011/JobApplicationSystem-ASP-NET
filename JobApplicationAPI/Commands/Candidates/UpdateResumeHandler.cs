using Domain.Repositories;
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
            throw new NotImplementedException();
        }
    }
}
