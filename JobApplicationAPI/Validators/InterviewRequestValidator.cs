using FluentValidation;
using JobApplicationAPI.DTOs.Interviews;

namespace JobApplicationAPI.Validators
{
    public class CreateInterviewRequestValidator : AbstractValidator<CreateInterviewRequest>
    {
        public CreateInterviewRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(50).WithMessage("Title must be at most 50 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(200).WithMessage("Description must be at most 200 characters");

            RuleFor(x => x.TimeScheduled)
                .GreaterThan(DateTime.Now).WithMessage("Time scheduled must be in the future");

            RuleFor(x => x.JobApplicationId)
                .GreaterThan(0).WithMessage("Job application is required");
        }
    }
}
