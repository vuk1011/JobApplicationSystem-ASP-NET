using Domain.Entities;
using Domain.Repositories;
using FluentValidation;
using JobApplicationAPI.Common.Exceptions;
using JobApplicationAPI.DTOs.Offers;
using MediatR;

namespace JobApplicationAPI.Commands.Offers
{
    public class CreateOfferHandler : IRequestHandler<CreateOfferCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly IValidator<CreateOfferRequest> _validator;

        public CreateOfferHandler(IUnitOfWork uow, IValidator<CreateOfferRequest> validator)
        {
            _uow = uow;
            _validator = validator;
        }

        public async Task<Unit> Handle(CreateOfferCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserId))
                throw new BadRequestException("Couldn't resolve user");

            var employee = await _uow.Employees.GetByAppUserIdAsync(request.UserId);
            if (employee is null)
                throw new ResourceNotFoundException("Couldn't find employee");

            var validationResult = await _validator.ValidateAsync(request.Request);
            if (!validationResult.IsValid)
                throw new BadRequestException(string.Join(" ", validationResult.Errors.Select(e => e.ErrorMessage)));

            var application = _uow.JobApplications.GetByIdWithDetails(request.Request.JobApplicationId);
            if (application is null)
                throw new ResourceNotFoundException("Job application not found");
            if (!application.IsManaged)
                throw new ConflictException("This job application is not managed");
            if (application.EmployeeId != employee.Id)
                throw new UnauthorizedException("Another employee is managing this job application");
            if (!JobApplicationStatusUtil.IsStatusChangeAllowed(application.Status, JobApplicationStatus.OFFERED))
                throw new ConflictException("Offer cannot be created in current status"));

            application.Status = JobApplicationStatus.OFFERED;
            _uow.JobApplications.Update(application);

            var offer = new Offer
            {
                Name = request.Request.Name,
                JobApplicationId = request.Request.JobApplicationId,
            };
            _uow.Offers.Add(offer);
            await _uow.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
