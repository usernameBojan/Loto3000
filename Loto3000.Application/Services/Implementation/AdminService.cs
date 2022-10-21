using AutoMapper;
using Loto3000.Application.Dto.Admin;
using Loto3000.Application.Dto.Transactions;
using Loto3000.Application.Repositories;
using Loto3000.Application.Utilities;
using Loto3000.Domain.Entities;
using Loto3000.Domain.Exceptions;

namespace Loto3000.Application.Services.Implementation
{
    public class AdminService : IAdminService
    {
        private readonly IRepository<User> userRepository;
        private readonly IRepository<Admin> adminRepository;
        private readonly IPasswordHasher passwordHasher;
        private readonly IMapper mapper;
        public AdminService(IRepository<User> userRepository, IRepository<Admin> adminRepository, IPasswordHasher passwordHasher, IMapper mapper)
        {
            this.adminRepository = adminRepository;
            this.userRepository = userRepository;
            this.passwordHasher = passwordHasher;
            this.mapper = mapper;
        }
        public AdminDto GetAdmin(int id)
        {
            var admin = adminRepository.GetById(id) ?? throw new NotFoundException();

            return mapper.Map<AdminDto>(admin);
        }
        public IEnumerable<AdminDto> GetAdmins()
        {
            var admins = adminRepository.Query().Select(a => mapper.Map<AdminDto>(a));

            return admins.ToList();
        }
        public AdminDto CreateAdmin(CreateAdminDto dto)
        {
            var admin = mapper.Map<Admin>(dto);

            if(userRepository.Query().Any(x => x.Username == dto.Username))
            {
                throw new ValidationException("Username already exists");
            }

            admin.Password = passwordHasher.HashToString(dto.Password);
            admin.Role = SystemRoles.Administrator;
            admin.IsVerified = true;

            adminRepository.Create(admin);

            return mapper.Map<AdminDto>(dto);
        }
        public void DeleteAdmin(int id)
        {
            var admin = adminRepository.GetById(id) ?? throw new NotFoundException();

            adminRepository.Delete(admin);
        }
        public IEnumerable<TransactionTrackerDto> GetAllTransactions()
        {
            throw new NotImplementedException();
        }
    }
}