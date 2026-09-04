using Domain.Entities;
using Domain.Repositories;
using System.Security.Claims;

namespace JobApplicationAPI.Services
{
    public class CurrentUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IUnitOfWork uow, IHttpContextAccessor httpContextAccessor)
        {
            _uow = uow;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<T?> GetCurrentAsync<T>() where T : GeneralUser
        {
            var appUserId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (appUserId is null)
            {
                return null;
            }

            return typeof(T) switch
            {
                _ when typeof(T) == typeof(Candidate) => await _uow.Candidates.GetByAppUserIdAsync(appUserId) as T,
                _ when typeof(T) == typeof(Employee) => await _uow.Employees.GetByAppUserIdAsync(appUserId) as T,
                _ => throw new NotSupportedException($"Unsupported user type '{typeof(T).Name}'"),
            };
        }
    }
}
