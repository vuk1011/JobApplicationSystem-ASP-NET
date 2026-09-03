using FluentValidation;
using JobApplicationAPI.DTOs.Offers;

namespace JobApplicationAPI.Validators
{
    public class CreateOfferRequestValidator : AbstractValidator<CreateOfferRequest>
    {
        public CreateOfferRequestValidator()
        {

        }
    }

    public class UpdateOfferRequestValidator : AbstractValidator<UpdateOfferRequest>
    {
        public UpdateOfferRequestValidator()
        {

        }
    }
}
