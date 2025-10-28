using JobApplication.Models.Domain;
using JobApplication.Models.DTOs;

namespace JobApplication.Repositories
{
    public interface IUserRepository
    {
        public Task<List<ApplicationUser>> GetAllUsersAsync();

        public Task<ApplicationUser?> FindUserByIdAsync(Guid id);

        public Task<ApplicationUser> DeleteUserAsync(ApplicationUser applicationUser);

    }
}
