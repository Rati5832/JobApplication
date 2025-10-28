using JobApplication.Data;
using JobApplication.Models.Domain;
using JobApplication.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace JobApplication.Repositories
{
    public class JobAppRepository : IJobAppRepository
    {
        private readonly JobDbContext appDbcontext;

        public JobAppRepository(JobDbContext appDbcontext)
        {
            this.appDbcontext = appDbcontext;
        }

        public async Task<ApplicationJob?> CreateAsync(ApplicationJob appJobDomain)
        {
            var alreadyApplied = await appDbcontext.Applications
                .AnyAsync(x => x.JobId == appJobDomain.JobId && x.ApplicantId == appJobDomain.ApplicantId);

            if (alreadyApplied)
            {
                return null;
            }

            await appDbcontext.Applications.AddAsync(appJobDomain);
            await appDbcontext.SaveChangesAsync();

            await appDbcontext.Entry(appJobDomain)
            .Reference(a => a.Job)
            .LoadAsync();

            return appJobDomain;
        }

        public async Task<List<ApplicationJob>> GetAllApplicationsAsync(Guid id)
        {
            return await appDbcontext.Applications.Where(x => x.ApplicantId == id).Include(a => a.Job).ToListAsync();
        }

        public async Task<List<ApplicationJob>> GetAllApplicationsForJobAsync(Guid id)
        {
            return await appDbcontext.Applications.Where(x => x.JobId == id).Include(a => a.Job).ToListAsync();
        }
    }
}
