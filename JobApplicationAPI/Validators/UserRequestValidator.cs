using FluentValidation;
using JobApplicationAPI.DTOs.Users;

namespace JobApplicationAPI.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {

        }
    }

    public class RegisterCandidateRequestValidator : AbstractValidator<RegisterCandidateRequest>
    {
        public RegisterCandidateRequestValidator()
        {

        }
    }

    public class RegisterEmployeeRequestValidator : AbstractValidator<RegisterEmployeeRequest>
    {
        public RegisterEmployeeRequestValidator()
        {

        }
    }

    public class UpdateCandidateRequestValidator : AbstractValidator<UpdateCandidateRequest>
    {
        public UpdateCandidateRequestValidator()
        {

        }
    }
}
