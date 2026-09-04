using MediatR;

namespace JobApplicationAPI.Commands.JobPostings
{
    public class UpdateJobPostingHandler : IRequestHandler<UpdateJobPostingCommand, Unit>
    {
        public UpdateJobPostingHandler()
        {
            
        }

        public async Task<Unit> Handle(UpdateJobPostingCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
