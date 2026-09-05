using Domain.Repositories;
using FluentValidation;
using JobApplicationAPI.DTOs.JobPostings;
using MediatR;

namespace JobApplicationAPI.Commands.JobPostings
{
    public class CreateJobPostingHandler : IRequestHandler<CreateJobPostingCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly IValidator<CreateJobPostingRequest> _validator;

        public CreateJobPostingHandler(IUnitOfWork uow, IValidator<CreateJobPostingRequest> validator)
        {
            _uow = uow;
            _validator = validator;
        }

        public async Task<Unit> Handle(CreateJobPostingCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
