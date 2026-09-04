using MediatR;

namespace JobApplicationAPI.Queries.JobApplications
{
    public class GetManagedJobApplicationsHandler : IRequestHandler<GetManagedJobApplicationsQuery, Unit>
    {
        public GetManagedJobApplicationsHandler()
        {
            
        }

        public async Task<Unit> Handle(GetManagedJobApplicationsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
