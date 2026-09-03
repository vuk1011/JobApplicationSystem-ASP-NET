using FluentValidation;
using JobApplicationAPI.DTOs.Users;

namespace JobApplicationAPI.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email must be well formatted");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required");
        }
    }

    public class RegisterCandidateRequestValidator : AbstractValidator<RegisterCandidateRequest>
    {
        public RegisterCandidateRequestValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required")
                .MaximumLength(30).WithMessage("First name must be at most 30 characters");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .MaximumLength(30).WithMessage("Last name must be at most 30 characters");

            RuleFor(x => x.Sex)
                .IsInEnum().WithMessage("Sex is required");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number is required")
                .Length(8, 16).WithMessage("Phone number must be between 8 and 16 characters")
                .Matches("^[0-9]+$").WithMessage("Phone number must contain only digits");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required")
                .MaximumLength(50).WithMessage("Address must be at most 50 characters");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email must be well formatted")
                .MaximumLength(50).WithMessage("Email must be at most 50 characters");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters");
        }
    }

    public class RegisterEmployeeRequestValidator : AbstractValidator<RegisterEmployeeRequest>
    {
        public RegisterEmployeeRequestValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required")
                .MaximumLength(30).WithMessage("First name must be at most 30 characters");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .MaximumLength(30).WithMessage("Last name must be at most 30 characters");

            RuleFor(x => x.Sex)
                .IsInEnum().WithMessage("Sex is required");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number is required")
                .Length(8, 16).WithMessage("Phone number must be between 8 and 16 characters")
                .Matches("^[0-9]+$").WithMessage("Phone number must contain only digits");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required")
                .MaximumLength(50).WithMessage("Address must be at most 50 characters");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email must be well formatted")
                .MaximumLength(50).WithMessage("Email must be at most 50 characters");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters");

            RuleFor(x => x.NationalId)
                .NotEmpty().WithMessage("National ID is required")
                .Length(10, 20).WithMessage("National ID must be between 10 and 20 characters");

            RuleFor(x => x.DateBorn)
                .LessThan(DateOnly.FromDateTime(DateTime.Today)).WithMessage("Date of birth must be in the past");

            RuleFor(x => x.DateHired)
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today)).WithMessage("Date of hiring must be in the past or present");

            RuleFor(x => x.CompanyId)
                .GreaterThan(0).WithMessage("Company is required");
        }
    }

    public class UpdateCandidateRequestValidator : AbstractValidator<UpdateCandidateRequest>
    {
        public UpdateCandidateRequestValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required")
                .MaximumLength(30).WithMessage("First name must be at most 30 characters");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .MaximumLength(30).WithMessage("Last name must be at most 30 characters");

            RuleFor(x => x.Sex)
                .IsInEnum().WithMessage("Sex is required");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number is required")
                .Length(8, 16).WithMessage("Phone number must be between 8 and 16 characters")
                .Matches("^[0-9]+$").WithMessage("Phone number must contain only digits");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required")
                .MaximumLength(50).WithMessage("Address must be at most 50 characters");
        }
    }
}
