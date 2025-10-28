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
    public class JobsController : ControllerBase
    {
        private readonly IJobRepository _jobsRepository;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;

        public JobsController(IJobRepository jobsRepository, IMapper mapper, IUserContextService userContextService)
        {
            _jobsRepository = jobsRepository;
            _mapper = mapper;
            _userContextService = userContextService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllJob([FromQuery]string? filterOn, [FromQuery]string? filterQuery, 
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 30)
        {
            var jobsDomain = await _jobsRepository.GetAllJobsAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);

            return Ok(_mapper.Map<List<JobResponseDto>>(jobsDomain));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetJobById([FromRoute] Guid id)
        {
            var jobsDomain = await _jobsRepository.GetJobByIdAsync(id);
            if(jobsDomain == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<JobResponseDto>(jobsDomain));
        }

        [HttpPost]
        [Authorize(Roles = "Employer, Admin")]
        public async Task<IActionResult> CreateJob([FromBody] JobRequestDto jobRequest)
        {
            var currentUserId = _userContextService.GetCurrentUserId();

            var jobsDomain = _mapper.Map<Job>(jobRequest);
            jobsDomain.PostedById = Guid.Parse(currentUserId);

            await _jobsRepository.CreateJobAsync(jobsDomain);

            return Ok(_mapper.Map<JobResponseDto>(jobsDomain));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Employer, Admin")]
        public async Task<IActionResult> UpdateJobById([FromBody] UpdateJobDto jobDto, [FromRoute] Guid id)
        {
            var jobDomain = await _jobsRepository.UpdateJobAsync(id, _mapper.Map<Job>(jobDto));
            if (jobDomain == null)
            {
                return NotFound($"The job with id {id} not Found!");
            }

            return Ok(_mapper.Map<JobResponseDto>(jobDomain));

        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Employer, Admin")]
        public async Task<IActionResult> DeleteJobById([FromRoute] Guid id)
        {
            var jobsDomain = await _jobsRepository.DeleteJobAsync(id);
            if(jobsDomain == null)
            {
                return NotFound($"Job With Given Id {id} Not Found!");
            }

            return Ok(_mapper.Map<JobResponseDto>(jobsDomain));
        }
    
    }
}
