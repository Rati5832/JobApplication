using AutoMapper;
using JobApplication.Models.Domain;
using JobApplication.Models.DTOs;
using JobApplication.Repositories;

namespace JobApplication.Services
{
    public class SavedJobsService : ISavedJobsService
    {
        private readonly ISavedJobsRepository _savedJobs;
        private readonly IJobRepository _jobs;
        private readonly IMapper _mapper;

        public SavedJobsService(ISavedJobsRepository savedJobs, IJobRepository jobs, IMapper mapper)
        {
            _savedJobs = savedJobs;
            _jobs = jobs;
            _mapper = mapper;
        }

        public async Task<ServiceResult<SavedJobsResponseDto>> SaveJobByIdAsync(Guid currentUserId, Guid jobId)
        {
            var getJobById = await _jobs.GetJobByIdAsync(jobId);
            if (getJobById == null)
                return ServiceResult<SavedJobsResponseDto>.Fail("NotFound");

            var savedJobDomain = new SavedJob()
            {
                JobId = getJobById.Id,
                UserId = currentUserId,
                SavedOn = DateTime.UtcNow
            };

            var saveJobInDb = await _savedJobs.SaveJob(savedJobDomain);
            if(saveJobInDb == null)
                return ServiceResult<SavedJobsResponseDto>.Fail("BadRequest");

            var dto = _mapper.Map<SavedJobsResponseDto>(savedJobDomain);

            return ServiceResult<SavedJobsResponseDto>.Ok(dto);
        }
    }
}
