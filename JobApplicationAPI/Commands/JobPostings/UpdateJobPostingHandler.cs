using Domain.Repositories;
using FluentValidation;
using JobApplicationAPI.DTOs.JobPostings;
using MediatR;

namespace JobApplicationAPI.Commands.JobPostings
{
    public class UpdateJobPostingHandler : IRequestHandler<UpdateJobPostingCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly IValidator<UpdateJobPostingRequest> _validator;

        public UpdateJobPostingHandler(IUnitOfWork uow, IValidator<UpdateJobPostingRequest> validator)
        {
            _uow = uow;
            _validator = validator;
        }

        public async Task<Unit> Handle(UpdateJobPostingCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
