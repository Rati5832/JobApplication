using JobApplication.Data;
using JobApplication.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JobApplication.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthDbContext dbContext;

        public UserRepository(AuthDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
        }


        public async Task<List<ApplicationUser>> GetAllUsersAsync()
        {
            return await dbContext.Users.ToListAsync();
        }

        public async Task<ApplicationUser?> FindUserByIdAsync(Guid id)
        {
            return await dbContext.Users.FindAsync(id.ToString());
        }

        public async Task<ApplicationUser> DeleteUserAsync(ApplicationUser applicationUser)
        {
            dbContext.Remove(applicationUser);
            await dbContext.SaveChangesAsync();

            return applicationUser;
        }
    }
}
