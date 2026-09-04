using MediatR;

namespace JobApplicationAPI.Commands.Offers
{
    public record DeleteOfferCommand : IRequest<Unit>;
}
