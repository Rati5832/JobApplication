using JobApplication.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace JobApplication.Data
{
    public class JobDbContext : DbContext
    {
        public JobDbContext(DbContextOptions<JobDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Job>  Jobs { get; set; }
        public DbSet<ApplicationJob> Applications { get; set; }
        public DbSet<SavedJob> SavedJobs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var testJob = new Job()
            {
                    Id = Guid.Parse("4f52c3d3-da40-4168-9f73-d0f8218c91df"),
                    Title = "Backend Developer",
                    CompanyName = "Best Company",
                    Salary = 50000,
                    Location = "Tbilisi, Georgia",
                    PostedById = Guid.Parse("b9d979d3-7c31-4a3a-a736-e94844026733")
            };

            modelBuilder.Entity<Job>().HasData(testJob);

        }

    }
}
