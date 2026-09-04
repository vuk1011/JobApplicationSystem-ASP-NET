using MediatR;

namespace JobApplicationAPI.Commands.Offers
{
    public class DeleteOfferHandler : IRequestHandler<DeleteOfferCommand, Unit>
    {
        public DeleteOfferHandler()
        {
            
        }

        public async Task<Unit> Handle(DeleteOfferCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
