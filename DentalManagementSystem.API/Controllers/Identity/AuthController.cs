using DentalManagementSystem.Application.Contracts.Identity;
using DentalManagementSystem.Application.DTO.Identity;
using DentalManagementSystem.Application.DTO.Responses;
using Microsoft.AspNetCore.Mvc;

namespace DentalManagementSystem.API.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(AuthRequest request)
        {
            var response = await _authService.Login(request);

            
            var result = new BaseApiResponse<AuthResponse>(response);
            return Ok(result);
            
        }

        [HttpPost("logout")]
        public async Task<ActionResult<BaseApiResponse<LogoutResponse>>> Logout()
        {
            var authHeader = Request.Headers["Authorization"].ToString();
            var token = authHeader.Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new BaseApiResponse<string>("Token is missing") { Success = false });
            }

            var response = await _authService.Logout(token);

            var result = new BaseApiResponse<LogoutResponse>(response);

            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegisterResponse>> Register(RegisterRequest request)
        {
            var response = await _authService.Register(request);

            var result = new BaseApiResponse<RegisterResponse>(response);

            if (!response.CreationStatus)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
