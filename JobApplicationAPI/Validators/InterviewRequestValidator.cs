using FluentValidation;
using JobApplicationAPI.DTOs.Interviews;

namespace JobApplicationAPI.Validators
{
    public class CreateInterviewRequestValidator : AbstractValidator<CreateInterviewRequest>
    {
        public CreateInterviewRequestValidator()
        {

        }
    }
}
