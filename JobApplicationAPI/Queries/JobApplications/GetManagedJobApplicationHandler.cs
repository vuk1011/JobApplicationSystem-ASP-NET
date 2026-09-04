using MediatR;

namespace JobApplicationAPI.Queries.JobApplications
{
    public class GetManagedJobApplicationHandler : IRequestHandler<GetManagedJobApplicationQuery, Unit>
    {
        public GetManagedJobApplicationHandler()
        {
            
        }

        public async Task<Unit> Handle(GetManagedJobApplicationQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
