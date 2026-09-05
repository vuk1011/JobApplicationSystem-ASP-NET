using Domain.Repositories;
using JobApplicationAPI.Common.Exceptions;
using JobApplicationAPI.DTOs.Interviews;
using JobApplicationAPI.Utilities;
using MediatR;

namespace JobApplicationAPI.Queries.Interviews
{
    public class GetInterviewsForEmployeeHandler : IRequestHandler<GetInterviewsForEmployeeQuery, List<InterviewDto>>
    {
        private readonly IUnitOfWork _uow;

        public GetInterviewsForEmployeeHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<List<InterviewDto>> Handle(GetInterviewsForEmployeeQuery request, CancellationToken cancellationToken)
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

            return _uow.Interviews.GetByJobApplicationId(request.JobApplicationId)
                .Select(InterviewMapper.ToDto)
                .ToList();
        }
    }
}
