using MediatR;

namespace JobApplicationAPI.Commands.JobApplications
{
    public class CreateJobApplicationHandler : IRequestHandler<CreateJobApplicationCommand, Unit>
    {
        public CreateJobApplicationHandler()
        {

        }

        public async Task<Unit> Handle(CreateJobApplicationCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
