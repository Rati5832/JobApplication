using JobApplication.Models.DTOs;

namespace JobApplication.Services
{
    public interface IUserContextService
    {
        string GetCurrentUserId();
        Task<UserProfileDto?> GetUserProfileDtoByCurrentId(string currentId);
    }
}
