using Domain.Entities;
using Domain.Repositories;
using FluentValidation;
using JobApplicationAPI.Common.Exceptions;
using JobApplicationAPI.DTOs.JobPostings;
using JobApplicationAPI.Utilities;
using MediatR;

namespace JobApplicationAPI.Commands.JobPostings
{
    public class CreateJobPostingHandler : IRequestHandler<CreateJobPostingCommand, JobPostingDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IValidator<CreateJobPostingRequest> _validator;

        public CreateJobPostingHandler(IUnitOfWork uow, IValidator<CreateJobPostingRequest> validator)
        {
            _uow = uow;
            _validator = validator;
        }

        public async Task<JobPostingDto> Handle(CreateJobPostingCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserId))
                throw new BadRequestException("Couldn't resolve user");

            var employee = await _uow.Employees.GetByAppUserIdAsync(request.UserId);
            if (employee is null)
                throw new ResourceNotFoundException("Couldn't find employee");

            var validationResult = await _validator.ValidateAsync(request.Request);
            if (!validationResult.IsValid)
                throw new BadHttpRequestException(string.Join(" ", validationResult.Errors.Select(e => e.ErrorMessage)));

            if (request.Request.DateOfExpiration < DateOnly.FromDateTime(DateTime.Today))
                throw new ConflictException("Invalid date of expiration");

            var jobPosting = new JobPosting
            {
                Title = request.Request.Title,
                Description = request.Request.Description,
                DateOfPublishing = DateOnly.FromDateTime(DateTime.Today),
                DateOfExpiration = request.Request.DateOfExpiration,
                CompanyId = employee.CompanyId,
            };
            _uow.JobPostings.Add(jobPosting);
            await _uow.SaveChangesAsync();

            var created = _uow.JobPostings.GetByIdWithCompany(jobPosting.Id)!;

            return JobPostingMapper.ToDto(created);
        }
    }
}
