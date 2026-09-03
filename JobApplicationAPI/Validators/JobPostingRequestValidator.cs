using FluentValidation;
using JobApplicationAPI.DTOs.JobPostings;

namespace JobApplicationAPI.Validators
{
    public class CreateJobPostingRequestValidator : AbstractValidator<CreateJobPostingRequest>
    {
        public CreateJobPostingRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(50).WithMessage("Title must be at most 50 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(3000).WithMessage("Description must be at most 3000 characters");

            RuleFor(x => x.DateExpires)
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today)).WithMessage("Date of expiration must be in the present or future");
        }
    }

    public class UpdateJobPostingRequestValidator : AbstractValidator<UpdateJobPostingRequest>
    {
        public UpdateJobPostingRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(50).WithMessage("Title must be at most 50 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(3000).WithMessage("Description must be at most 3000 characters");

            RuleFor(x => x.DateExpires)
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today)).WithMessage("Date of expiration must be in the present or future");
        }
    }
}
