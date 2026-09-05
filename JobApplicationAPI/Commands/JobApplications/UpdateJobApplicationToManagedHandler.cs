using Domain.Repositories;
using FluentValidation;
using JobApplicationAPI.DTOs.JobApplications;
using MediatR;

namespace JobApplicationAPI.Commands.JobApplications
{
    public class UpdateJobApplicationToManagedHandler : IRequestHandler<UpdateJobApplicationToManagedCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly IValidator<ManageJobApplicationRequest> _validator;

        public UpdateJobApplicationToManagedHandler(IUnitOfWork uow, IValidator<ManageJobApplicationRequest> validator)
        {
            _uow = uow;
            _validator = validator;
        }

        public async Task<Unit> Handle(UpdateJobApplicationToManagedCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
