using AutoMapper;
using JobApplication.Models.Domain;
using JobApplication.Models.DTOs;
using JobApplication.Repositories;
using JobApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SavedJobsController : ControllerBase
    {
        private readonly ISavedJobsRepository _savedJobs;
        private readonly IJobRepository _jobs;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly ISavedJobsService _savedJobsService;

        public SavedJobsController(ISavedJobsRepository savedJobs, IJobRepository jobs, IMapper mapper, IUserContextService userContextService, ISavedJobsService savedJobsService)
        {
            _savedJobs = savedJobs;
            _jobs = jobs;
            _mapper = mapper;
            _userContextService = userContextService;
            _savedJobsService = savedJobsService;
        }

        [HttpPost]
        [Authorize(Roles = "Job Seeker, Admin")]
        public async Task<IActionResult> SaveJob([FromBody] SavedJobsRequestDto savedJobsRequestDto)
        {
            var currentUserId = Guid.Parse(_userContextService.GetCurrentUserId());
            var jobsResponseDto = await _savedJobsService.SaveJobByIdAsync(currentUserId, savedJobsRequestDto.JobId);

            if(!jobsResponseDto.Success)
            {
                return jobsResponseDto.Error switch
                {
                    "NotFound" => StatusCode(StatusCodes.Status404NotFound, new {message = $"Job with ID {savedJobsRequestDto.JobId} does not exist"} ),
                    "BadRequest" => StatusCode(StatusCodes.Status400BadRequest, new { message = "You already applied for this job" }),
                    _ => BadRequest(jobsResponseDto.Error)
                };
            }

            return Ok(jobsResponseDto.Data);
        }

        [HttpGet]
        [Authorize(Roles = "Job Seeker, Admin")]
        public async Task<IActionResult> GetSavedJobs()
        {
            var currentUserId = _userContextService.GetCurrentUserId();

            var savedJobDomain = await _savedJobs.GetJobs(Guid.Parse(currentUserId));

            return Ok(_mapper.Map<List<SavedJobsResponseDto>>(savedJobDomain));

        }

        [HttpDelete]
        [Authorize(Roles = "Job Seeker, Admin")]
        public async Task<IActionResult> DeleteSavedJob(Guid id)
        {
            var savedJobDomain = await _savedJobs.DeleteSavedJobById(id);
            if(savedJobDomain == null)
            {
                return NotFound($"Job With Id: {id} Not Found");
            }

            return Ok(_mapper.Map<SavedJobsResponseDto>(savedJobDomain));

        }

    }
}
