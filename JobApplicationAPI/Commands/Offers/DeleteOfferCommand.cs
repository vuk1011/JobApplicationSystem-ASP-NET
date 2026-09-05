using MediatR;

namespace JobApplicationAPI.Commands.Offers
{
    public record DeleteOfferCommand(string? UserId, long OfferId) : IRequest<Unit>;
}
