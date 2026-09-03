using FluentValidation;
using JobApplicationAPI.DTOs.JobApplications;

namespace JobApplicationAPI.Validators
{
    public class UpdateJobApplicationStatusRequestValidator : AbstractValidator<UpdateJobApplicationStatusRequest>
    {
        public UpdateJobApplicationStatusRequestValidator()
        {

        }
    }

    public class SubmitJobApplicationRequestValidator : AbstractValidator<SubmitJobApplicationRequest>
    {
        public SubmitJobApplicationRequestValidator()
        {

        }
    }

    public class ManageJobApplicationRequestValidator : AbstractValidator<ManageJobApplicationRequest>
    {
        public ManageJobApplicationRequestValidator()
        {

        }
    }
}
