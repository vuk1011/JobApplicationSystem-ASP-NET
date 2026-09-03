using FluentValidation;
using JobApplicationAPI.DTOs.JobApplications;

namespace JobApplicationAPI.Validators
{
    public class UpdateJobApplicationStatusRequestValidator : AbstractValidator<UpdateJobApplicationStatusRequest>
    {
        public UpdateJobApplicationStatusRequestValidator()
        {
            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Status is required");
        }
    }

    public class SubmitJobApplicationRequestValidator : AbstractValidator<SubmitJobApplicationRequest>
    {
        public SubmitJobApplicationRequestValidator()
        {
            RuleFor(x => x.JobPostingId)
                .GreaterThan(0).WithMessage("Job posting is required");
        }
    }

    public class ManageJobApplicationRequestValidator : AbstractValidator<ManageJobApplicationRequest>
    {
        public ManageJobApplicationRequestValidator()
        {
            RuleFor(x => x.JobApplicationId)
                .GreaterThan(0).WithMessage("Job application is required");
        }
    }
}
