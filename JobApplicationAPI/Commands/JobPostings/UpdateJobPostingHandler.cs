using Domain.Repositories;
using FluentValidation;
using JobApplicationAPI.Common.Exceptions;
using JobApplicationAPI.DTOs.JobPostings;
using MediatR;

namespace JobApplicationAPI.Commands.JobPostings
{
    public class UpdateJobPostingHandler : IRequestHandler<UpdateJobPostingCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly IValidator<UpdateJobPostingRequest> _validator;

        public UpdateJobPostingHandler(IUnitOfWork uow, IValidator<UpdateJobPostingRequest> validator)
        {
            _uow = uow;
            _validator = validator;
        }

        public async Task<Unit> Handle(UpdateJobPostingCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserId))
                throw new BadRequestException("Couldn't resolve user");

            var employee = await _uow.Employees.GetByAppUserIdAsync(request.UserId);
            if (employee is null)
                throw new ResourceNotFoundException("Couldn't find employee");

            var validationResult = await _validator.ValidateAsync(request.Request);
            if (!validationResult.IsValid)
                throw new BadRequestException(string.Join(" ", validationResult.Errors.Select(e => e.ErrorMessage)));

            var jobPosting = _uow.JobPostings.GetByIdWithCompany(request.JobPostingId);
            if (jobPosting is null)
                throw new ResourceNotFoundException("Job posting not found");
            if (jobPosting.CompanyId != employee.CompanyId)
                throw new UnauthorizedException("This job posting isn't associated with your company");
            if (request.Request.DateOfExpiration < DateOnly.FromDateTime(DateTime.Today))
                throw new ConflictException("Expiration date cannot be set before current time");

            jobPosting.Title = request.Request.Title;
            jobPosting.Description = request.Request.Description;
            jobPosting.DateOfExpiration = request.Request.DateOfExpiration;
            _uow.JobPostings.Update(jobPosting);
            await _uow.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
