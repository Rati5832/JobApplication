
using JobApplication.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobApplication.Data
{
    public class AuthDbContext : IdentityDbContext<ApplicationUser>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var jobSeekerRoleId = "76dd3561-d137-490b-a08d-5459a97f9191";
            var employeerRoleId = "70e013d9-a175-4789-8ff8-b189513965c4";
            var adminRoleId = "8d03b03d-b57d-4291-b347-025e32ebf4c4";

            var roles = new List<IdentityRole>()
            {
                new IdentityRole {
                Id = jobSeekerRoleId,
                ConcurrencyStamp = jobSeekerRoleId,
                Name = "Job Seeker",
                NormalizedName = "Job Seeker".ToUpper()

                },

                new IdentityRole {
                Id = employeerRoleId,
                ConcurrencyStamp = employeerRoleId,
                Name = "Employer",
                NormalizedName = "Employer".ToUpper()
                },

                new IdentityRole {
                Id = adminRoleId,
                ConcurrencyStamp = adminRoleId,
                Name = "Admin",
                NormalizedName = "Admin".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

        }

    }
}
