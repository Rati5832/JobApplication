using JobApplication.Models.DTOs;

namespace JobApplication.Services
{
    public interface IUserService
    {
        public Task<ApplicationUserResponseDto?> DeleteUserById(Guid id);
        public Task<List<ApplicationUserResponseDto>> GetAllUsers();

    }
}
