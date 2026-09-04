using MediatR;

namespace JobApplicationAPI.Commands.Interviews
{
    public class DeleteInterviewHandler : IRequestHandler<DeleteInterviewCommand, Unit>
    {
        public DeleteInterviewHandler()
        {
            
        }

        public async Task<Unit> Handle(DeleteInterviewCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
