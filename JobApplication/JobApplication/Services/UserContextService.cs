using JobApplication.Models.Domain;
using JobApplication.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace JobApplication.Services
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserContextService(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public string GetCurrentUserId()
        {
            var user = _httpContextAccessor.HttpContext?.User
                ?? throw new InvalidOperationException("No active HttpContext or User not found.");

            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                throw new InvalidOperationException("User ID claim not found.");

            return userId;
        }

        public async Task<UserProfileDto?> GetUserProfileDtoByCurrentId(string currentId)
        {
            var user = await _userManager.FindByIdAsync(currentId);

            if (user == null)
                return null;
            
            var roles = await _userManager.GetRolesAsync(user);

            var profile = new UserProfileDto()
            {
                FullName = user.FullName,
                Email = user.Email!,
                Role = roles.FirstOrDefault()!
            };

            return profile;
        }
    }
}
