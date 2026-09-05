using Domain.Repositories;
using Infrastructure.Identity;
using JobApplicationAPI.Common.Exceptions;
using JobApplicationAPI.DTOs.Users;
using JobApplicationAPI.Utilities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace JobApplicationAPI.Queries.JobApplications
{
    public class GetCandidateForJobApplicationHandler : IRequestHandler<GetCandidateForJobApplicationQuery, CandidateDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly UserManager<AppUser> _userManager;

        public GetCandidateForJobApplicationHandler(IUnitOfWork uow, UserManager<AppUser> userManager)
        {
            _uow = uow;
            _userManager = userManager;
        }

        public async Task<CandidateDto> Handle(GetCandidateForJobApplicationQuery request, CancellationToken cancellationToken)
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
            if (candidate is null)
                throw new ResourceNotFoundException("Candidate not found");

            var appUser = await _userManager.FindByIdAsync(candidate.AppUserId);
            if (appUser is null)
                throw new ResourceNotFoundException("Candidate not found");

            return CandidateMapper.ToDto(candidate, appUser);
        }
    }
}
