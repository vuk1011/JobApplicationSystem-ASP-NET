using Domain.Repositories;
using JobApplicationAPI.Common.Exceptions;
using JobApplicationAPI.Utilities;
using MediatR;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JobApplicationAPI.Queries.JobPostings
{
    public class GetJobPostingsExportForEmployeeHandler : IRequestHandler<GetJobPostingsExportForEmployeeQuery, string>
    {
        private readonly IUnitOfWork _uow;

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter() },
        };

        public GetJobPostingsExportForEmployeeHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<string> Handle(GetJobPostingsExportForEmployeeQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserId))
                throw new BadRequestException("Couldn't resolve user");

            var employee = await _uow.Employees.GetByAppUserIdAsync(request.UserId);
            if (employee is null)
                throw new ResourceNotFoundException("Couldn't find employee");

            var jobPostings = _uow.JobPostings.GetAllByCompanyId(employee.CompanyId)
                .Select(JobPostingMapper.ToDto)
                .ToList();

            return JsonSerializer.Serialize(jobPostings, JsonOptions);
        }
    }
}
