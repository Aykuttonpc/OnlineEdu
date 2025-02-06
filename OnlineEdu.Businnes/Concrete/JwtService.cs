using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineEdu.Businnes.Abstract;
using OnlineEdu.Businnes.Configurations;
using OnlineEdu.DTO.DTOs.LoginDtos;
using OnlineEdu.Entity.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.Businnes.Concrete
{
    public class JwtService : IJwtService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly JwtTokenOptions _tokenOptions;

        public JwtService(UserManager<AppUser> userManager, IOptions<JwtTokenOptions> tokenOptions)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _tokenOptions = tokenOptions.Value ?? throw new ArgumentNullException(nameof(tokenOptions));
        }

        public async Task<LoginResponseDto> CreateTokenAsync(AppUser user)
        {
            var symnetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.Key));
            var userRoles = await _userManager.GetRolesAsync(user);

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("fullName", user.FirstName + user.LastName),
            };

            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(_tokenOptions.ExpireToMinute),
                signingCredentials: new SigningCredentials(symnetricSecurityKey, SecurityAlgorithms.HmacSha256)
            );

            var handler = new JwtSecurityTokenHandler();
            var responseDto = new LoginResponseDto
            {
                Token = handler.WriteToken(jwtSecurityToken),
                ExprieDate = DateTime.UtcNow.AddMinutes(_tokenOptions.ExpireToMinute)
            };
            return responseDto;
        }
    }
}
