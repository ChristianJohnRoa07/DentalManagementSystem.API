using DentalManagementSystem.Application.Contracts.Identity;
using DentalManagementSystem.Application.DTO.Identity;
using DentalManagementSystem.Application.Exceptions;
using DentalManagementSystem.Identity.DbContext;
using DentalManagementSystem.Identity.Models;
using DentalManagementSystem.Identity.Services.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DentalManagementSystem.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationUserDbContext _userDbContext;
        private readonly JwtSettings _jwtSettings;

        public AuthService(
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationUserDbContext userDbContext,
            IOptions<JwtSettings> jwtSettings
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userDbContext = userDbContext;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<AuthResponse> Login(AuthRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);

            if (user == null)
            {
                throw NotFoundException.Username(request.Username);
            }

            var passwordChecker = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (passwordChecker.Succeeded == false)
            {
                throw BadRequestException.InvalidPassword(request.Username);
            }

            var roles = await _userManager.GetRolesAsync(user);

            var userRole = roles.FirstOrDefault();

            JwtSecurityToken jwtSecurityToken = await JwtUtility.GenerateToken(user, _userManager, _jwtSettings);

            var response = new AuthResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = userRole,
                Email = user.Email,
                UserName = user.UserName,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
            };

            return response;
        }

        public async Task<LogoutResponse> Logout(string token)
        {

            if (string.IsNullOrWhiteSpace(token))
            {
                throw new BadRequestException("Token is required to logout.");
            }

            try
            {
                await _signInManager.SignOutAsync();

                var jwtToken = JwtUtility.VerifyAndReadToken(token);
                var expiryDate = jwtToken.ValidTo;

                JwtUtility.BlacklistToken(token, expiryDate, _userDbContext);

                return new LogoutResponse
                {
                    LogoutStatus = true,
                    Message = "Successfully logged out."
                };
            }
            catch (BadRequestException)
            {
                
                throw;
            }
            catch (Exception ex)
            {
                throw new BadRequestException("An unexpected error occurred while processing your logout request.");
            }
        }

        public async Task<RegisterResponse> Register(RegisterRequest request)
        {
            var user = new ApplicationUser
            {
                UserName = request.Username,
                Email = request.Email,
                EmailConfirmed = true, // To create email service for email confirmation
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            var role = await _roleManager.FindByIdAsync(request.RoleId);

            if (role == null)
            {
                throw NotFoundException.Role(request.RoleId);
            }

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, role.Name);

                return new RegisterResponse()
                {
                    CreationStatus = true,
                    UserId = user.Id,
                };
            }
            else
            {
                var errorMessages = result.Errors.Select(e => e.Description).ToList();
                return new RegisterResponse()
                {
                    CreationStatus = false,
                    Errors = errorMessages
                };
            }
        }
    }
}
