using Loto3000.Application.Dto.Login;
using Loto3000.Domain.Exceptions;
using Loto3000.Application.Repositories;
using Loto3000.Application.Utilities;
using Loto3000.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace Loto3000.Application.Services.Implementation
{
    public class LoginService : ILoginService
    {
        private readonly IRepository<Admin> adminRepository;
        private readonly IRepository<Player> playerRepository;
        private readonly IRepository<SuperAdmin> superAdminRepository;
        private readonly IPasswordHasher hasher;
        private readonly IConfiguration configuration;

        public LoginService(
            IRepository<Admin> adminRepository,
            IRepository<Player> playerRepository,
            IRepository<SuperAdmin> superAdminRepository,
            IPasswordHasher hasher,
            IConfiguration configuration
            )
        {
            this.adminRepository = adminRepository;
            this.playerRepository = playerRepository;
            this.superAdminRepository = superAdminRepository;
            this.hasher = hasher;
            this.configuration = configuration;
        }
        public TokenDto Authenticate(LoginDto loginDto)
        {            
            var admins = adminRepository.Query();
            var players = playerRepository.Query();

            #region super-admin testing logic only
            var superAdmin = superAdminRepository.Query().FirstOrDefault() ?? throw new NotFoundException();
            if (superAdmin.Username == loginDto.Username && superAdmin.Password == loginDto.Password)
            {
                return GenericAuthentication<SuperAdmin>.Authenticate(superAdmin, configuration, superAdmin.Role);
            }
            #endregion

            IEnumerable<User> users = CombineUsersForRegisterAndLogin.Combine(players, admins);

            var user = users.Where(x => x.Username == loginDto.Username).FirstOrDefault() ?? throw new NotFoundException("User not found");

            if (!hasher.Verify(loginDto.Password, user.Password))
            {
                throw new ValidationException("Wrong password?");
            }

            return GenericAuthentication<User>.Authenticate(user, configuration, user.Role);
        }
    }
}