using JobApplication.Models.Domain;
using JobApplication.Models.DTOs;
using JobApplication.Repositories;
using Microsoft.AspNetCore.Identity;

namespace JobApplication.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserRepository userRepository;

        public UserService(UserManager<ApplicationUser> userManager, IUserRepository userRepository)
        {
            this.userManager = userManager;
            this.userRepository = userRepository;
        }

        public async Task<ApplicationUserResponseDto?> DeleteUserById(Guid id)
        {
            var user = await userRepository.FindUserByIdAsync(id);
            if(user == null)
            {
                return null;
            }

            var getRoles = await userManager.GetRolesAsync(user);

            await userRepository.DeleteUserAsync(user);

            var dto = new ApplicationUserResponseDto()
            {
                Id = user.Id,
                FullName = user.FullName,
                UserName = user.UserName,
                Email = user.Email,
                Role = getRoles.FirstOrDefault()
            };

            return dto;

        }

        public async Task<List<ApplicationUserResponseDto>> GetAllUsers()
        {
            var usersDomain = await userRepository.GetAllUsersAsync();

            var result = new List<ApplicationUserResponseDto>();
            foreach (var user in usersDomain)
            {
                var roles = await userManager.GetRolesAsync(user);
                result.Add(new ApplicationUserResponseDto
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    UserName = user.UserName,
                    Email = user.Email,
                    Role = roles.FirstOrDefault()
                });
            }
            return result;

        }
    }
}
