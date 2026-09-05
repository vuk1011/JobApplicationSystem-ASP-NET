using Domain.Entities;
using Domain.Repositories;
using FluentValidation;
using JobApplicationAPI.Common.Exceptions;
using JobApplicationAPI.DTOs.JobApplications;
using MediatR;

namespace JobApplicationAPI.Commands.JobApplications
{
    public class CreateJobApplicationHandler : IRequestHandler<CreateJobApplicationCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly IValidator<SubmitJobApplicationRequest> _validator;

        public CreateJobApplicationHandler(IUnitOfWork uow, IValidator<SubmitJobApplicationRequest> validator)
        {
            _uow = uow;
            _validator = validator;
        }

        public async Task<Unit> Handle(CreateJobApplicationCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserId))
                throw new BadRequestException("Couldn't resolve user");

            var candidate = await _uow.Candidates.GetByAppUserIdAsync(request.UserId);
            if (candidate is null)
                throw new ResourceNotFoundException("Couldn't find candidate");

            var validationResult = await _validator.ValidateAsync(request.Request);
            if (!validationResult.IsValid)
                throw new BadRequestException(string.Join(" ", validationResult.Errors.Select(e => e.ErrorMessage)));

            var jobPosting = _uow.JobPostings.GetByIdWithCompany(request.Request.JobPostingId);
            if (jobPosting is null)
                throw new ResourceNotFoundException("Couldn't find job posting");

            if (_uow.JobApplications.existsByCandidateIdAndJobPostingId(candidate.Id, request.Request.JobPostingId))
                throw new ConflictException("You already applied for this job posting");

            if (jobPosting.IsClosed)
                throw new BadRequestException("Job posting closed");

            var application = new JobApplication
            {
                DateOfSubmission = DateOnly.FromDateTime(DateTime.Today),
                Status = JobApplicationStatus.SUBMITTED,
                JobPostingId = jobPosting.Id,
                CandidateId = candidate.Id,
            };
            _uow.JobApplications.Add(application);
            await _uow.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
