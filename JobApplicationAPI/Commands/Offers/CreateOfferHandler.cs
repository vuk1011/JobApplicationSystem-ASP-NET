using Domain.Repositories;
using FluentValidation;
using JobApplicationAPI.DTOs.Offers;
using MediatR;

namespace JobApplicationAPI.Commands.Offers
{
    public class CreateOfferHandler : IRequestHandler<CreateOfferCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly IValidator<CreateOfferRequest> _validator;

        public CreateOfferHandler(IUnitOfWork uow, IValidator<CreateOfferRequest> validator)
        {
            _uow = uow;
            _validator = validator;
        }

        public async Task<Unit> Handle(CreateOfferCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
