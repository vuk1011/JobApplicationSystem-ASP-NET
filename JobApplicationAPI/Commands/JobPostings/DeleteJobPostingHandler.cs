using MediatR;

namespace JobApplicationAPI.Commands.JobPostings
{
    public class DeleteJobPostingHandler : IRequestHandler<DeleteJobPostingCommand, Unit>
    {
        public DeleteJobPostingHandler()
        {
            
        }

        public async Task<Unit> Handle(DeleteJobPostingCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
