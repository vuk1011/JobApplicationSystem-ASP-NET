using Domain.Repositories;
using FluentValidation;
using JobApplicationAPI.DTOs.JobApplications;
using MediatR;

namespace JobApplicationAPI.Commands.JobApplications
{
    public class CreateJobApplicationHandler : IRequestHandler<CreateJobApplicationCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly IValidator<SubmitJobApplicationRequest> _validator;

        public CreateJobApplicationHandler(IUnitOfWork uow, IValidator<SubmitJobApplicationRequest> validator)
        {
            _uow = uow;
            _validator = validator;
        }

        public async Task<Unit> Handle(CreateJobApplicationCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
