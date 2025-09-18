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
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterationDTO dto)
        {
            try
            {
                var user = await _authService.RegisterUserAsync(dto);
                return Ok(new { Message = "User registered successfully", User = user.Username, user.Email });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] loginDTO dto)
        {
            var user = await _authService.ValidateUserAsync(dto.Email, dto.Password);
            if (user == null)
                return Unauthorized(new { Error = "Invalid email or password" });

            var token = _authService.GenerateJwtToken(user);
            return Ok(new { Token = token, Username = user.Username, user.Email });
        }
    }
}
   
