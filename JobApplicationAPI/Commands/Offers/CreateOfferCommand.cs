using JobApplicationAPI.DTOs.Offers;
using MediatR;

namespace JobApplicationAPI.Commands.Offers
{
    public record CreateOfferCommand(string? UserId, CreateOfferRequest Request) : IRequest<Unit>;
}
