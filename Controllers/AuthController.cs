using FBR_Invoicing_Integration.DTOs.Auth;
using FBR_Invoicing_Integration.Interfaces;
using FBR_Invoicing_Integration.Services;
using Microsoft.AspNetCore.Mvc;

namespace FBR_Invoicing_Integration.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterationDTO dto)
        {
            _logger.LogInformation("Register request received for email: {Email}", dto.Email);
            try
            {
                var user = await _authService.RegisterUserAsync(dto);
                _logger.LogInformation("User {Username} registered successfully with email {Email}", user.Username, user.Email);
                return Ok(new { Message = "User registered successfully", User = user.Username, user.Email });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during registration for email {Email}", dto.Email);
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] loginDTO dto)
        {
            _logger.LogInformation("Login attempt for email: {Email}", dto.Email);

            var user = await _authService.ValidateUserAsync(dto.Email, dto.Password);
            if (user == null)
            {
                _logger.LogWarning("Invalid login attempt for email: {Email}", dto.Email);

                return Unauthorized(new { Error = "Invalid email or password" });
            }
            var token = _authService.GenerateJwtToken(user);
            _logger.LogInformation("User {Username} logged in successfully at {Time}", user.Username, DateTime.UtcNow);
            return Ok(new { Token = token, Username = user.Username, user.Email });
        }
    }
}
   
