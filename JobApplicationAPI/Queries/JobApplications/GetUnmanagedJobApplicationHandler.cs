using MediatR;

namespace JobApplicationAPI.Queries.JobApplications
{
    public class GetUnmanagedJobApplicationHandler : IRequestHandler<GetUnmanagedJobApplicationQuery, Unit>
    {
        public GetUnmanagedJobApplicationHandler()
        {
            
        }

        public async Task<Unit> Handle(GetUnmanagedJobApplicationQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
