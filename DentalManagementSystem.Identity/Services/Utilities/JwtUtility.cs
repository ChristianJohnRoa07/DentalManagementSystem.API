using DentalManagementSystem.Application.DTO.Identity;
using DentalManagementSystem.Application.Exceptions;
using DentalManagementSystem.Identity.DbContext;
using DentalManagementSystem.Identity.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DentalManagementSystem.Identity.Services.Utilities
{
    public static class JwtUtility
    {
        private static readonly JwtSecurityTokenHandler TokenHandler = new();

        public static JwtSecurityToken VerifyAndReadToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new BadRequestException("Token string cannot be empty.");
            }
            
            if (!TokenHandler.CanReadToken(token))
            {
                throw new BadRequestException("The provided token is malformed or invalid.");
            }

            return TokenHandler.ReadJwtToken(token);
        }

        public static async Task BlacklistToken(string token,DateTime expiryDate, ApplicationUserDbContext dbContext)
        {
            var alreadyBlacklisted = await dbContext.BlacklistedTokens.AnyAsync(b => b.Token == token);

            if (!alreadyBlacklisted)
            {
                var blacklistedToken = new BlacklistedToken
                {
                    Token = token,
                    ExpiryDate = expiryDate
                };
                
                dbContext.BlacklistedTokens.Add(blacklistedToken);
                await dbContext.SaveChangesAsync();
            }
        }

        public async static Task<JwtSecurityToken> GenerateToken(ApplicationUser user, UserManager<ApplicationUser> userManager, JwtSettings jwtSettings)
        {
            var userClaims = await userManager.GetClaimsAsync(user);
            var roles = await userManager.GetRolesAsync(user);

            var roleClaims = roles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials
            );

            return jwtSecurityToken;
        }
    }
}
