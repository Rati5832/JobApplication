using AutoMapper;
using JobApplication.Models.Domain;
using JobApplication.Models.DTOs;
using JobApplication.Repositories;
using System;

namespace JobApplication.Services
{
    public class JobApplicationService : IJobApplicationService
    {
        private readonly IJobAppRepository _jobAppRepository;
        private readonly IMapper _mapper;
        private readonly IJobRepository _jobRepository;

        public JobApplicationService(IJobAppRepository jobAppRepository, IMapper mapper, IJobRepository jobRepository)
        {
            _jobAppRepository = jobAppRepository;
            _mapper = mapper;
            _jobRepository = jobRepository;
        }

        public async Task<ServiceResult<List<ApplicationJobResponseDto>>> GetAppliedAppsForJobWithId(Guid jobId, Guid currentUserId, bool isAdmin)
        {
            var job = await _jobRepository.GetJobByIdAsync(jobId);
            if (job == null)
                return ServiceResult<List<ApplicationJobResponseDto>>.Fail("NotFound");

            if (job.PostedById != currentUserId && !isAdmin)
                return ServiceResult<List<ApplicationJobResponseDto>>.Fail("Forbidden");

            var apps = await _jobAppRepository.GetAllApplicationsForJobAsync(jobId);
            var dto = _mapper.Map<List<ApplicationJobResponseDto>>(apps);

            return ServiceResult<List<ApplicationJobResponseDto>>.Ok(dto);
        }
    }
}
