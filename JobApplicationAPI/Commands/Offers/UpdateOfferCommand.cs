using JobApplicationAPI.DTOs.Offers;
using MediatR;

namespace JobApplicationAPI.Commands.Offers
{
    public record UpdateOfferCommand(string? UserId, long OfferId, UpdateOfferRequest Request) : IRequest<Unit>;
}
