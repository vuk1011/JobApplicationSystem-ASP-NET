using MediatR;

namespace JobApplicationAPI.Commands.Offers
{
    public class UpdateOfferHandler : IRequestHandler<UpdateOfferCommand, Unit>
    {
        public UpdateOfferHandler()
        {
            
        }

        public async Task<Unit> Handle(UpdateOfferCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
