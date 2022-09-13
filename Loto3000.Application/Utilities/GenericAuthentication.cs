using Loto3000.Application.Dto.Login;
using Loto3000.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Loto3000.Application.Utilities
{
    public static class GenericAuthentication<T> where T : User, IEntity
    {
        public static TokenDto Authenticate(T user, IConfiguration configuration, string role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var algorithm = SecurityAlgorithms.HmacSha256;

            var credentials = new SigningCredentials(key, algorithm);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Role, role),
            };

            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                                             configuration["Jwt:Audience"],
                                             claims,
                                             expires: DateTime.Now.AddMinutes(45),
                                             signingCredentials: credentials);

            TokenDto tokenDto = new TokenDto()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };

            return tokenDto;
        }
    }
}