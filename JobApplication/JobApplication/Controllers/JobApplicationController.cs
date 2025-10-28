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
    public class JobApplicationController : ControllerBase
    {
        private readonly IJobAppRepository _jobAppRepository;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly IJobApplicationService _jobApplicationService;

        public JobApplicationController(IJobAppRepository jobAppRepository, IMapper mapper, IUserContextService userContextService, IJobApplicationService jobApplicationService)
        {
            _jobAppRepository = jobAppRepository;
            _mapper = mapper;
            _userContextService = userContextService;
            _jobApplicationService = jobApplicationService;
        }

        [HttpPost]
        [Authorize(Roles = "Job Seeker, Admin")]
        public async Task<IActionResult> CreateJobApplication([FromBody] ApplicationJobRequestDto jobAppDto)
        {
            var currentUserId = _userContextService.GetCurrentUserId();

            var appJobDomain = new ApplicationJob
            {
                ApplicantId = Guid.Parse(currentUserId),
                JobId = jobAppDto.JobId,
                ResumeUrl = jobAppDto.ResumeUrl,
                AppliedOn = DateTime.UtcNow
            };


            var result = await _jobAppRepository.CreateAsync(appJobDomain);
            if (result == null)
            {
                return NotFound(new { message = $"You already applied job with Id: {appJobDomain.JobId}" });
            }

            var response = new ApplicationJobResponseDto
            {
                Id = result.Id,
                ApplicantId = result.ApplicantId,
                JobId = result.JobId,
                JobTitle = result.Job?.Title ?? string.Empty,
                CompanyName = result.Job?.CompanyName ?? string.Empty,
                ResumeUrl = result.ResumeUrl,
                AppliedOn = result.AppliedOn
            };

            return Ok(response);
        }

        [HttpGet]
        [Authorize(Roles = "Job Seeker, Admin")]
        public async Task<IActionResult> GetUserApplications()
        {
            var currentUserId = _userContextService.GetCurrentUserId();

            var result = await _jobAppRepository.GetAllApplicationsAsync(Guid.Parse(currentUserId));
  
            return Ok(_mapper.Map<List<ApplicationJobResponseDto>>(result));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Employer, Admin")]
        public async Task<IActionResult> GetAppliedAppsForJobWithId([FromRoute] Guid id)
        {
            var currentUserId = Guid.Parse(_userContextService.GetCurrentUserId());

            var isAdmin = User.IsInRole("Admin");

            var result = await _jobApplicationService.GetAppliedAppsForJobWithId(id, currentUserId, isAdmin);

            if (!result.Success)
            {
                return result.Error switch
                {
                    "NotFound" => NotFound(),
                    "Forbidden" => StatusCode(StatusCodes.Status403Forbidden, new { message = "The job must be posted by current employer or admin!" }),
                    _ => BadRequest(result.Error)
                };
            }

            return Ok(result.Data);
        }
    }
}
