using MediatR;

namespace JobApplicationAPI.Commands.JobApplications
{
    public class UpdateJobApplicationStatusHandler : IRequestHandler<UpdateJobApplicationStatusCommand, Unit>
    {
        public UpdateJobApplicationStatusHandler()
        {

        }

        public async Task<Unit> Handle(UpdateJobApplicationStatusCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
