using Domain.Repositories;
using MediatR;

namespace JobApplicationAPI.Commands.JobPostings
{
    public class DeleteJobPostingHandler : IRequestHandler<DeleteJobPostingCommand, Unit>
    {
        private readonly IUnitOfWork _uow;

        public DeleteJobPostingHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Unit> Handle(DeleteJobPostingCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
