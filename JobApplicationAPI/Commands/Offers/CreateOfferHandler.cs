using MediatR;

namespace JobApplicationAPI.Commands.Offers
{
    public class CreateOfferHandler : IRequestHandler<CreateOfferCommand, Unit>
    {
        public CreateOfferHandler()
        {

        }

        public async Task<Unit> Handle(CreateOfferCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
