using Domain.Repositories;
using FluentValidation;
using JobApplicationAPI.DTOs.JobApplications;
using MediatR;

namespace JobApplicationAPI.Commands.JobApplications
{
    public class UpdateJobApplicationStatusHandler : IRequestHandler<UpdateJobApplicationStatusCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly IValidator<UpdateJobApplicationStatusRequest> _validator;

        public UpdateJobApplicationStatusHandler(IUnitOfWork uow, IValidator<UpdateJobApplicationStatusRequest> validator)
        {
            _uow = uow;
            _validator = validator;
        }

        public async Task<Unit> Handle(UpdateJobApplicationStatusCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
