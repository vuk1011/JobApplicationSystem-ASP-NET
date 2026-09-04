using MediatR;

namespace JobApplicationAPI.Commands.Candidates
{
    public class DeleteResumeHandler : IRequestHandler<DeleteResumeCommand, Unit>
    {
        public DeleteResumeHandler()
        {

        }

        public async Task<Unit> Handle(DeleteResumeCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
