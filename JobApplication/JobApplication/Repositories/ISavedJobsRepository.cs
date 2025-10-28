using JobApplication.Models.Domain;

namespace JobApplication.Repositories
{
    public interface ISavedJobsRepository
    {
        public Task<SavedJob?> SaveJob(SavedJob savedBody);
        public Task<SavedJob?> DeleteSavedJobById(Guid id);
        public Task<List<SavedJob>> GetJobs(Guid id);

    }
}
