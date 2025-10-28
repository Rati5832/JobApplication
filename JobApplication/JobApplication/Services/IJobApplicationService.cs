using JobApplication.Models.Domain;
using JobApplication.Models.DTOs;

namespace JobApplication.Services
{
    public interface IJobApplicationService
    {
        public Task<ServiceResult<List<ApplicationJobResponseDto>>> GetAppliedAppsForJobWithId(Guid jobId, Guid currentUserId, bool isAdmin);

    }
}
