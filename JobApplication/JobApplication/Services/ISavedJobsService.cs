using JobApplication.Models.DTOs;

namespace JobApplication.Services
{
    public interface ISavedJobsService
    {
        public Task<ServiceResult<SavedJobsResponseDto>> SaveJobByIdAsync(Guid currentUserId, Guid jobId);
    }
}
