using MediatR;

namespace JobApplicationAPI.Commands.Offers
{
    public record CreateOfferCommand : IRequest<Unit>;
}
