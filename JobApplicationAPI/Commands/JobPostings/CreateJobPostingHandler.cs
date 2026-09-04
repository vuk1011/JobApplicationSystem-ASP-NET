using MediatR;

namespace JobApplicationAPI.Commands.JobPostings
{
    public class CreateJobPostingHandler : IRequestHandler<CreateJobPostingCommand, Unit>
    {
        public CreateJobPostingHandler()
        {
            
        }

        public async Task<Unit> Handle(CreateJobPostingCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
