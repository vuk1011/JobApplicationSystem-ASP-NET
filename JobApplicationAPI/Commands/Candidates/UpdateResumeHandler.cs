using MediatR;

namespace JobApplicationAPI.Commands.Candidates
{
    public class UpdateResumeHandler : IRequestHandler<UpdateResumeCommand, Unit>
    {
        public UpdateResumeHandler()
        {

        }

        public async Task<Unit> Handle(UpdateResumeCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
