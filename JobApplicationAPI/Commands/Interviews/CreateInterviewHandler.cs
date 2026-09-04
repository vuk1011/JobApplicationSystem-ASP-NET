using MediatR;

namespace JobApplicationAPI.Commands.Interviews
{
    public class CreateInterviewHandler : IRequestHandler<CreateInterviewCommand, Unit>
    {
        public CreateInterviewHandler()
        {

        }

        public async Task<Unit> Handle(CreateInterviewCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
