using MediatR;

namespace JobApplicationAPI.Commands.JobPostings
{
    public class ImportJobPostingsHandler : IRequestHandler<ImportJobPostingsCommand, Unit>
    {
        public ImportJobPostingsHandler()
        {
            
        }

        public async Task<Unit> Handle(ImportJobPostingsCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
