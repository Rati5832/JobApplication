using JobApplication.Data;
using JobApplication.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace JobApplication.Repositories
{
    public class SavedJobsRepository : ISavedJobsRepository
    {
        private readonly JobDbContext jobDbContext;

        public SavedJobsRepository(JobDbContext jobDbContext)
        {
            this.jobDbContext = jobDbContext;
        }

        public async Task<SavedJob?> SaveJob(SavedJob savedBody)
        {
            var exists = await jobDbContext.SavedJobs.Include(a => a.Job).AnyAsync(x => x.JobId == savedBody.JobId && x.UserId == savedBody.UserId);

            if(exists)
            { return null; };

            await jobDbContext.SavedJobs.AddAsync(savedBody);
            await jobDbContext.SaveChangesAsync();

            return savedBody;
        }

        public async Task<SavedJob?> DeleteSavedJobById(Guid id)
        {
            var savedJob = await jobDbContext.SavedJobs.Include(a=> a.Job).FirstOrDefaultAsync(x => x.JobId == id);
            if(savedJob == null)
            {
                return null;
            }

            jobDbContext.Remove(savedJob);
            await jobDbContext.SaveChangesAsync();

            return savedJob;
        }

        public async Task<List<SavedJob>> GetJobs(Guid id)
        {
            return await jobDbContext.SavedJobs.Include(x=> x.Job).Where(a => a.UserId == id).ToListAsync();
        }
    }
}
