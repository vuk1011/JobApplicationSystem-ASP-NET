using Domain.Repositories;
using FluentValidation;
using JobApplicationAPI.DTOs.Offers;
using MediatR;

namespace JobApplicationAPI.Commands.Offers
{
    public class UpdateOfferHandler : IRequestHandler<UpdateOfferCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly IValidator<UpdateOfferRequest> _validator;

        public UpdateOfferHandler(IUnitOfWork uow, IValidator<UpdateOfferRequest> validator)
        {
            _uow = uow;
            _validator = validator;
        }

        public async Task<Unit> Handle(UpdateOfferCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
