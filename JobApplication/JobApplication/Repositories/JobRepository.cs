using JobApplication.Data;
using JobApplication.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace JobApplication.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly JobDbContext jobsDatabase;

        public JobRepository(JobDbContext jobsDatabase)
        {
            this.jobsDatabase = jobsDatabase;
        }


        public async Task<List<Job>> GetAllJobsAsync(string? filterOn, string? filterQuery,
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 30)
        {

            var jobs = jobsDatabase.Jobs.AsQueryable();

            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Title", StringComparison.OrdinalIgnoreCase))
                {
                    jobs = jobs.Where(x => x.Title.Contains(filterQuery));
                }
                else if (filterOn.Equals("CompanyName", StringComparison.OrdinalIgnoreCase))
                {
                    jobs = jobs.Where(x => x.CompanyName.Contains(filterQuery));
                }
                else if (filterOn.Equals("Location", StringComparison.OrdinalIgnoreCase))
                {
                    jobs = jobs.Where(x => x.Location.Contains(filterQuery));
                }
            }

            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Title", StringComparison.OrdinalIgnoreCase))
                {
                    jobs = isAscending ? jobs.OrderBy(x => x.Title) : jobs.OrderByDescending(x => x.Title);
                }
                else if (sortBy.Equals("CompanyName", StringComparison.OrdinalIgnoreCase))
                {
                    jobs = isAscending ? jobs.OrderBy(x => x.CompanyName) : jobs.OrderByDescending(x => x.CompanyName);
                }
                else if (sortBy.Equals("Location", StringComparison.OrdinalIgnoreCase))
                {
                    jobs = isAscending ? jobs.OrderBy(x => x.Location) : jobs.OrderByDescending(x => x.Location);
                }
            }

            var skipResults = (pageNumber - 1) * pageSize;

            return await jobs.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<Job?> GetJobByIdAsync(Guid id)
        {
            var jobDomain = await jobsDatabase.Jobs.FirstOrDefaultAsync(x => x.Id == id);

            if (jobDomain == null)
            {
                return null;
            }

            return jobDomain;
            
        }

        public async Task<Job> CreateJobAsync(Job jobRequest)
        {
            await jobsDatabase.Jobs.AddAsync(jobRequest);
            await jobsDatabase.SaveChangesAsync();

            return jobRequest;
        }

        public async Task<Job?> UpdateJobAsync(Guid id, Job job)
        {
            var jobsDomain =  await jobsDatabase.Jobs.FirstOrDefaultAsync(x => x.Id == id);
            if (jobsDomain == null)
            {
                return null;
            }

            jobsDomain.Title = job.Title;
            jobsDomain.CompanyName = job.CompanyName;
            jobsDomain.Salary = job.Salary;
            jobsDomain.Location = job.Location;

            await jobsDatabase.SaveChangesAsync();

            return jobsDomain;
        }

        public async Task<Job?> DeleteJobAsync(Guid id)
        {
            var job = await jobsDatabase.Jobs.FirstOrDefaultAsync(x => x.Id == id);
            if (job == null)
            {
                return null;
            }

            jobsDatabase.Jobs.Remove(job);
            await jobsDatabase.SaveChangesAsync();

            return job;
        }
    }
}
