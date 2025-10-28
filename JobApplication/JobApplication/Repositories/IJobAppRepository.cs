using JobApplication.Models.Domain;
using JobApplication.Models.DTOs;

namespace JobApplication.Repositories
{
    public interface IJobAppRepository
    {
        public Task<ApplicationJob?> CreateAsync(ApplicationJob jobApplication);
        public Task<List<ApplicationJob>> GetAllApplicationsAsync(Guid id);
        public Task<List<ApplicationJob>> GetAllApplicationsForJobAsync(Guid id);
    }
}
