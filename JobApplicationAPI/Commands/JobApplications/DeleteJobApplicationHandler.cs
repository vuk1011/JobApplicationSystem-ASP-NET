using MediatR;

namespace JobApplicationAPI.Commands.JobApplications
{
    public class DeleteJobApplicationHandler : IRequestHandler<DeleteJobApplicationCommand, Unit>
    {
        public DeleteJobApplicationHandler()
        {
            
        }

        public async Task<Unit> Handle(DeleteJobApplicationCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
