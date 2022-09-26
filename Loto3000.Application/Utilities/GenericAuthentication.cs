using Loto3000.Application.Dto.Login;
using Loto3000.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Loto3000.Application.Utilities
{
    public static class GenericAuthentication<T> where T : IEntity
    {
        public static TokenDto Authenticate(int id, string name, string surname, string role, IConfiguration configuration)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var algorithm = SecurityAlgorithms.HmacSha256;

            var credentials = new SigningCredentials(key, algorithm);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Surname, surname),
                new Claim(ClaimTypes.Role, role),
            };

            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                                             configuration["Jwt:Audience"],
                                             claims,
                                             expires: DateTime.Now.AddMinutes(45),
                                             signingCredentials: credentials);

            TokenDto tokenDto = new ()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
            };

            return tokenDto;
        }
    }
}