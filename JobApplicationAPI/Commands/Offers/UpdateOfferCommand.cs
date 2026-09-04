using MediatR;

namespace JobApplicationAPI.Commands.Offers
{
    public record UpdateOfferCommand : IRequest<Unit>;
}
