using JobApplication.Models.Domain;
using JobApplication.Models.DTOs;
using JobApplication.Repositories;
using JobApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenRepository _tokenRepository;
        private readonly IUserContextService _userContextService;

        public AuthController(UserManager<ApplicationUser> userManager, ITokenRepository tokenRepository, IUserContextService userContextService)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
            _userContextService = userContextService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new ApplicationUser
            {
                FullName = registerRequestDto.FullName,
                UserName = registerRequestDto.Email,
                Email = registerRequestDto.Email
            };

            var identityResult = await _userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if(identityResult.Succeeded)
                if(registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    identityResult = await _userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("User Was Registered.");
                    }
                }
            if (!identityResult.Succeeded)
            {
                return BadRequest(identityResult.Errors);
            }
            return BadRequest("Something Went Wrong");
        }

        [HttpPost]
        [Route("login")] 
        public async Task<IActionResult> LoginUser([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await _userManager.FindByEmailAsync(loginRequestDto.Email);

            if(user != null)
            {
                var checkPasswordResult = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if(checkPasswordResult)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    if(roles != null)
                    {
                        var jwtToken = _tokenRepository.createJWTToken(user, roles.ToList());
                        var response = new LoginResponseDto()
                        {
                            JwtToken = jwtToken
                        };

                        return Ok(response);

                    }
                }

            }
            return BadRequest("Username or password was incorrect");
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> GetUserProfile()
        {
            var currentUserId = _userContextService.GetCurrentUserId();

            var userDto = await _userContextService.GetUserProfileDtoByCurrentId(currentUserId);
            if (userDto == null)
            {
                return Unauthorized(new { message = "User not found or no longer exists." });
            }

            return Ok(userDto);

        }

    }
}
