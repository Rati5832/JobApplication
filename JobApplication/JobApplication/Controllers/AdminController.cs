using AutoMapper;
using JobApplication.Models.DTOs;
using JobApplication.Repositories;
using JobApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IJobRepository _jobRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public AdminController(IJobRepository jobRepository, IUserRepository userRepository, IMapper mapper, IUserService userService)
        {
            _jobRepository = jobRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpGet]
        [Route("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var dto = await _userService.GetAllUsers();

            return Ok(dto);
        }

        [HttpGet]
        [Route("jobs")]
        public async Task<IActionResult> GetAllJobs([FromQuery]string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 30)
        {

            var jobsDomain = await _jobRepository.GetAllJobsAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);

            return Ok(_mapper.Map<List<JobResponseDto>>(jobsDomain));
        }

        [HttpDelete]
        [Route("delete-user/{id:Guid}")]
        public async Task<IActionResult> DeleteUserById(Guid id)
        {
            var dto = await _userService.DeleteUserById(id);

            return dto is null ? NotFound($"User With Id {id} Not Found!") : Ok(dto);
        }
    }
}
