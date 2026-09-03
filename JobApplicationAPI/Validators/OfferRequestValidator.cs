using FluentValidation;
using JobApplicationAPI.DTOs.Offers;

namespace JobApplicationAPI.Validators
{
    public class CreateOfferRequestValidator : AbstractValidator<CreateOfferRequest>
    {
        public CreateOfferRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(50).WithMessage("Name must be at most 50 characters");

            RuleFor(x => x.JobApplicationId)
                .GreaterThan(0).WithMessage("Job application is required");
        }
    }

    public class UpdateOfferRequestValidator : AbstractValidator<UpdateOfferRequest>
    {
        public UpdateOfferRequestValidator()
        {

        }
    }
}
