using DentalClinic.Interfaces;
using DentalClinic.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DentalClinic.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly Jwt _jwt;

        private readonly IUnitOfWork _unitOfWork;

        public AuthService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IOptions<Jwt> jwt, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
            _unitOfWork = unitOfWork;
        }

        public async Task<string> AddUserToRoleAsync(AddRole model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user is null || !await _roleManager.RoleExistsAsync(model.Role))
            {
                return "Invalid user id or role";
            }
            if (await _userManager.IsInRoleAsync(user, model.Role))
            {
                return "User already assigned to this role!";
            }
            var result = await _userManager.AddToRoleAsync(user, model.Role);

            return result.Succeeded ? string.Empty : "Something went wrong";
        }

        public async Task<Auth> GetTokenAsync(RequestToken model)
        {
            var authModel = new Auth();

            var user = await _unitOfWork.Users.FindAsync(p => p.PhoneNumber == model.PhoneNumber);

            if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authModel.Message = "Phone Number or password is incorrect";
                return authModel;
            }
            var jwtSecurityToken = await CreateJwtToken(user);
            var rolesList = await _userManager.GetRolesAsync(user);

            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.PhoneNumber = model.PhoneNumber;
            authModel.FirstName = user.FirstName;
            authModel.FourthName = user.FourthName;
            authModel.Email = user.Email;
            authModel.Roles = [.. rolesList];
            authModel.UserId = user.Id;

            if (user.RefreshTokens is not null && user.RefreshTokens.Any(t => t.IsActive))
            {
                var activeRefreshToken = user.RefreshTokens.FirstOrDefault(p => p.IsActive);

                authModel.RefreshToken = activeRefreshToken?.Token;
                authModel.RefreshTokenExpiration = activeRefreshToken?.ExpiresOn;
            }
            else
            {
                var refreshToken = GenerateRefreshToken();
                authModel.RefreshToken = refreshToken.Token;
                authModel.RefreshTokenExpiration = refreshToken?.ExpiresOn;
                user.RefreshTokens?.Add(refreshToken);
                await _userManager.UpdateAsync(user);
            }
            return authModel;

        }

        public async Task<Auth> RefreshTokenAsync(string token)
        {
            var authModel = new Auth();

            var user = await _userManager.Users.SingleOrDefaultAsync(p => p.RefreshTokens.Any(t => t.Token == token));

            if (user == null)
            {
                authModel.Message = "Invalid token";
                return authModel;

            }
            var refreshToken = user.RefreshTokens.Single(t => t.Token == token);

            if (!refreshToken.IsActive)
            {
                authModel.Message = "Inactive token";
                return authModel;
            }
            refreshToken.RevokedOn = DateTime.UtcNow;
            var newRefreshToken = GenerateRefreshToken();
            user.RefreshTokens.Add(newRefreshToken);
            await _userManager.UpdateAsync(user);
            var jwtToken = await CreateJwtToken(user);
            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            authModel.PhoneNumber = user.PhoneNumber;
            authModel.FirstName = user.FirstName;
            authModel.FourthName = user.FourthName;
            authModel.Email = user.Email;
            var roles = await _userManager.GetRolesAsync(user);
            authModel.Roles = [.. roles];

            authModel.RefreshToken = newRefreshToken.Token;
            authModel.RefreshTokenExpiration = newRefreshToken.ExpiresOn;

            return authModel;
        }

        public async Task<Auth> RegisterAsync(RegisterPatient model)
        {
            if (model.Email is not null && await _userManager.FindByEmailAsync(model.Email) is not null)
            {
                return new Auth { Message = "Email is already registered!" };
            }

            if (model.PhoneNumber is not null && await _unitOfWork.Users.FindAsync(p => p.PhoneNumber == model.PhoneNumber) is not null)
            {
                return new Auth { Message = "Phone number is already registered!" };
            }

            var user = model.Adapt<User>();
            user.UserName = $"@{user.FirstName}_@{user.FourthName}";

            await _userManager.UpdateAsync(user);

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description}";
                }
                return new Auth { Message = errors };
            }
            var jwtSecurityToken = await CreateJwtToken(user);
            var refreshToken = GenerateRefreshToken();
            user.RefreshTokens?.Add(refreshToken);
            await _userManager.UpdateAsync(user);
            var rolesList = await _userManager.GetRolesAsync(user);

            Auth auth = new()
            {
                IsAuthenticated = true,
                UserId = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                FirstName = user.FirstName,
                FourthName = user.FourthName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                RefreshToken = refreshToken.Token,
                RefreshTokenExpiration = refreshToken.ExpiresOn,
            };
            return auth;
        }

        public async Task<bool> RevokeTokenAsync(User user, string token)
        {
            if (user.RefreshTokens is null)
            {
                return false;
            }

            var refreshToken = user.RefreshTokens.Single(t => t.Token == token);

            if (!refreshToken.IsActive)
            {
                return false;
            }

            refreshToken.RevokedOn = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);
            return true;


        }

        public async Task<JwtSecurityToken> CreateJwtToken(User user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("roles", role));
            }
            IEnumerable<Claim> claims;
            if (user.Email is not null && user.PhoneNumber is not null)
            {
                claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.PhoneNumber),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("uid", user.Id),

                }
                .Union(userClaims)
                .Union(roleClaims);
            }
            else
            {
                claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub,user.FirstName),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim("uid",user.Id)
                }
                .Union(userClaims)
                .Union(roleClaims);
            }
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }

        private RefreshToken GenerateRefreshToken()
        {
            var randomNumber = new Byte[23];

            using var generator = RandomNumberGenerator.Create();

            generator.GetBytes(randomNumber);

            return new RefreshToken()
            {
                Token = Convert.ToBase64String(randomNumber),
                ExpiresOn = DateTime.UtcNow.AddDays(_jwt.DurationInDays),
                CreatedAt = DateTime.UtcNow,
            };
        }
    }
}
