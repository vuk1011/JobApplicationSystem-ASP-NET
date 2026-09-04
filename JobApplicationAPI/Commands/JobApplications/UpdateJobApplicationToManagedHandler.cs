using MediatR;

namespace JobApplicationAPI.Commands.JobApplications
{
    public class UpdateJobApplicationToManagedHandler : IRequestHandler<UpdateJobApplicationToManagedCommand, Unit>
    {
        public UpdateJobApplicationToManagedHandler()
        {

        }

        public async Task<Unit> Handle(UpdateJobApplicationToManagedCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
