using FluentValidation;
using JobApplicationAPI.DTOs.JobPostings;

namespace JobApplicationAPI.Validators
{
    public class CreateJobPostingRequestValidator : AbstractValidator<CreateJobPostingRequest>
    {
        public CreateJobPostingRequestValidator()
        {

        }
    }

    public class UpdateJobPostingRequestValidator : AbstractValidator<UpdateJobPostingRequest>
    {
        public UpdateJobPostingRequestValidator()
        {

        }
    }
}
