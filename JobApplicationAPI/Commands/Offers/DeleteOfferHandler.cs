using Domain.Repositories;
using MediatR;

namespace JobApplicationAPI.Commands.Offers
{
    public class DeleteOfferHandler : IRequestHandler<DeleteOfferCommand, Unit>
    {
        private readonly IUnitOfWork _uow;

        public DeleteOfferHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Unit> Handle(DeleteOfferCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
