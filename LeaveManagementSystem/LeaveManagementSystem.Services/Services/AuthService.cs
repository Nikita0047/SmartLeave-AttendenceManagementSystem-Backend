using LeaveManagementSystem.Core.Dtos.Auth;
using LeaveManagementSystem.Core.Dtos.User;
using LeaveManagementSystem.Core.Entities;
using LeaveManagementSystem.Services.Services.IServices;
using LeaveManagmentSystem.Data.Repository.UnitOfWorks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystem.Services.Services
{
    // Application/Services/AuthService.cs
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _uow;
        private readonly IConfiguration _config;

        public AuthService(IUnitOfWork uow, IConfiguration config)
        {
            _uow = uow;
            _config = config;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto dto)
        {
            // 1 — find user by email
            var user = await _uow.Users.GetByEmailAsync(dto.Username);  
            if (user is null || !user.IsActive)
                throw new UnauthorizedAccessException("Invalid email or password.");

            // 2 — verify password
            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid email or password.");

            // 3 — load permissions for this role from DB
            var permissions = await _uow.RolePermissions
                .GetPermissionNamesForRoleAsync(user.Role);

            // 4 — build JWT
            var token = GenerateJwtToken(user, permissions);
            var refreshToken = GenerateRefreshToken();

            return new LoginResponseDto
            {
                AccessToken = token,
                RefreshToken = refreshToken,
                Expiration = DateTime.UtcNow.AddMinutes(
                    double.Parse(_config["Jwt:ExpiryMinutes"]!)),
                User = new UserSummeryDto
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    Role = user.Role.ToString(),
                    DepartmentId = user.DepartmentId,
                    DepartmentName = user.Department.Name
                },
                Permissions = permissions
            };
        }

        public async Task<LoginResponseDto> RefreshTokenAsync(RefreshTokenRequestDto dto)
        {
            // for simplicity — in production store refresh tokens in DB
            // and validate here before issuing new JWT
            throw new NotImplementedException("Refresh token logic goes here.");
        }

        public async Task LogoutAsync(Guid userId)
        {
            // invalidate refresh token in DB if you store them
            // for now — client just discards the token
            await Task.CompletedTask;
        }

        // ── private helpers ──────────────────────────────────────

        private string GenerateJwtToken(User user, List<string> permissions)
        {
            var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email,          user.Email),
            new(ClaimTypes.Role,           user.Role.ToString()),
            new("department_id",           user.DepartmentId.ToString()),
            new("full_name",               user.FullName),
        };

            // add each permission as a separate claim
            claims.AddRange(permissions.Select(p => new Claim("permission", p)));

            var key = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(_config["Jwt:Secret"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                              double.Parse(_config["Jwt:ExpiryMinutes"]!)),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static string GenerateRefreshToken()
        {
            var bytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
}
