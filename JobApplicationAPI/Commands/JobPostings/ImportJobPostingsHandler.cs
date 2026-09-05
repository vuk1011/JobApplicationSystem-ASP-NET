using Domain.Repositories;
using MediatR;

namespace JobApplicationAPI.Commands.JobPostings
{
    public class ImportJobPostingsHandler : IRequestHandler<ImportJobPostingsCommand, Unit>
    {
        private readonly IUnitOfWork _uow;

        public ImportJobPostingsHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Unit> Handle(ImportJobPostingsCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
