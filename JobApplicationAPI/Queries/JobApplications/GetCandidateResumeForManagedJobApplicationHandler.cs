using Domain.Repositories;
using JobApplicationAPI.Common.Exceptions;
using MediatR;

namespace JobApplicationAPI.Queries.JobApplications
{
    public class GetCandidateResumeForManagedJobApplicationHandler : IRequestHandler<GetCandidateResumeForManagedJobApplicationQuery, byte[]>
    {
        private readonly IUnitOfWork _uow;

        public GetCandidateResumeForManagedJobApplicationHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<byte[]> Handle(GetCandidateResumeForManagedJobApplicationQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserId))
                throw new BadRequestException("Couldn't resolve user");

            var employee = await _uow.Employees.GetByAppUserIdAsync(request.UserId);
            if (employee is null)
                throw new ResourceNotFoundException("Couldn't find employee");

            var application = _uow.JobApplications.GetByIdWithDetails(request.JobApplicationId);
            if (application is null)
                throw new ResourceNotFoundException("Job application not found");
            if (!application.IsManaged)
                throw new ConflictException("This job application is not managed");
            if (application.EmployeeId != employee.Id)
                throw new UnauthorizedException("Another employee is managing this job application");

            var candidate = _uow.Candidates.Find(c => c.Id == application.CandidateId).FirstOrDefault();
            if (candidate is null || candidate.Resume is null)
                throw new ResourceNotFoundException("Resume not found");

            return candidate.Resume;
        }
    }
}
