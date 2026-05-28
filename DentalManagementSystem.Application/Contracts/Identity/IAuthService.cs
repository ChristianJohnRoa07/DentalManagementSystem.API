using DentalManagementSystem.Application.DTO.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagementSystem.Application.Contracts.Identity
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(AuthRequest request);
        Task<RegisterResponse> Register(RegisterRequest request);
        Task<LogoutResponse> Logout(string token);
    }
}
