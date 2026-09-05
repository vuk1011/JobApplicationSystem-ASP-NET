using Domain.Repositories;
using MediatR;

namespace JobApplicationAPI.Commands.Interviews
{
    public class DeleteInterviewHandler : IRequestHandler<DeleteInterviewCommand, Unit>
    {
        private readonly IUnitOfWork _uow;

        public DeleteInterviewHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Unit> Handle(DeleteInterviewCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
