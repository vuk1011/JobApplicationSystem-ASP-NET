using Domain.Repositories;
using FluentValidation;
using JobApplicationAPI.DTOs.Offers;
using MediatR;

namespace JobApplicationAPI.Commands.Interviews
{
    public class CreateInterviewHandler : IRequestHandler<CreateInterviewCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly IValidator<CreateOfferRequest> _validator;

        public CreateInterviewHandler(IUnitOfWork uow, IValidator<CreateOfferRequest> validator)
        {
            _uow = uow;
            _validator = validator;
        }

        public async Task<Unit> Handle(CreateInterviewCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
