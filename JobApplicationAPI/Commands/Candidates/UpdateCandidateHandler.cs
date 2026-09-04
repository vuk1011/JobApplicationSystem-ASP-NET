using MediatR;

namespace JobApplicationAPI.Commands.Candidates
{
    public class UpdateCandidateHandler : IRequestHandler<UpdateCandidateCommand, Unit>
    {
        public UpdateCandidateHandler()
        {
            
        }

        public async Task<Unit> Handle(UpdateCandidateCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
