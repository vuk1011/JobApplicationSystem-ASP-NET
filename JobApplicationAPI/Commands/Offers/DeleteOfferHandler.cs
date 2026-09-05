using Domain.Repositories;
using JobApplicationAPI.Common.Exceptions;
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
            if (string.IsNullOrEmpty(request.UserId))
                throw new BadRequestException("Couldn't resolve user");

            var employee = await _uow.Employees.GetByAppUserIdAsync(request.UserId);
            if (employee is null)
                throw new ResourceNotFoundException("Couldn't find employee");

            var offer = _uow.Offers.GetByIdWithJobApplication(request.OfferId);
            if (offer is null)
                throw new ResourceNotFoundException("Offer not found");
            if (offer.JobApplication.EmployeeId != employee.Id)
                throw new UnauthorizedException("Another employee is managing the associated job application for the offer");
            if (offer.Accepted is not null)
                throw new ConflictException("Offer cannot be deleted after it got accepted or rejected");

            _uow.Offers.Remove(offer);
            await _uow.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
