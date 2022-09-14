using Loto3000.Application.Dto.Login;
using Loto3000.Domain.Exceptions;
using Loto3000.Application.Repositories;
using Loto3000.Application.Utilities;
using Loto3000.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Loto3000.Application.Services.Implementation
{
    public class LoginService : ILoginService
    {
        private readonly IRepository<User> userRepository;
        private readonly IRepository<SuperAdmin> superAdminRepository;
        private readonly IPasswordHasher hasher;
        private readonly IConfiguration configuration;

        public LoginService(IRepository<User> userRepository, IRepository<SuperAdmin> superAdminRepository, IPasswordHasher hasher, IConfiguration configuration)
        {
            this.userRepository = userRepository;
            this.superAdminRepository = superAdminRepository;
            this.hasher = hasher;
            this.configuration = configuration;
        }
        public TokenDto Authenticate(LoginDto loginDto)
        {
            #region super-admin testing logic only
            var superAdmin = superAdminRepository.Query().FirstOrDefault() ?? throw new NotFoundException();
            if (superAdmin.Username == loginDto.Username && superAdmin.Password == loginDto.Password)
            {
                return GenericAuthentication<SuperAdmin>.Authenticate(superAdmin.Id, superAdmin.Username, superAdmin.Username, superAdmin.Role,configuration);
            } // BAD PRATICE, ONLY FOR LOCAL TESTING PURPOSES 
            #endregion

            var user = userRepository.Query().Where(x => x.Username == loginDto.Username).FirstOrDefault() ?? throw new NotFoundException("User not found");

            if (!hasher.Verify(loginDto.Password, user.Password))
            {
                throw new ValidationException("Wrong password?");
            }
            #region verification - comment this code block to login without email verification
            if (!user.IsVerified)
            {
                throw new ValidationException("User not verified");
            }
            #endregion

            return GenericAuthentication<User>.Authenticate(user.Id, user.FirstName, user.LastName, user.Role, configuration);
        }
    }
}