using Domain.Repositories;
using MediatR;

namespace JobApplicationAPI.Queries.Interviews
{
    public class GetInterviewsForEmployeeHandler : IRequestHandler<GetInterviewsForEmployeeQuery, Unit>
    {
        private readonly IUnitOfWork _uow;

        public GetInterviewsForEmployeeHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Unit> Handle(GetInterviewsForEmployeeQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
