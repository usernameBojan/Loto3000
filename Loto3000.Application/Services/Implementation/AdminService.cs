using AutoMapper;
using Loto3000.Application.Dto.Admin;
using Loto3000.Application.Dto.Player;
using Loto3000.Application.Dto.Transactions;
using Loto3000.Application.Dto.PlayerAccountManagment;
using Loto3000.Application.Dto.Tickets;
using Loto3000.Application.Repositories;
using Loto3000.Domain.Entities;
using Isopoh.Cryptography.Argon2;

namespace Loto3000.Application.Services.Implementation
{
    public class AdminService : IAdminService
    {
        private readonly IRepository<Admin> adminRepository;
        private readonly IRepository<TransactionTracker> transactionsRepository;
        private readonly IMapper mapper;

        public AdminService(IRepository<Admin> adminRepository, IRepository<TransactionTracker> transactionsRepository, IMapper mapper)
        {
            this.adminRepository = adminRepository;
            this.transactionsRepository = transactionsRepository;
            this.mapper = mapper;
        }
        public AdminDto GetAdmin(int id)
        {
            var admin = adminRepository.GetById(id);
            if (admin == null)
            {
                throw new Exception("Admin not found");
            }

            return mapper.Map<AdminDto>(admin);
        }
        public IEnumerable<AdminDto> GetAdmins()
        {
            var admins = adminRepository.GetAll()
                                        .Select(a => mapper.Map<AdminDto>(a));

            return admins.ToList();
        }
        public IEnumerable<TransactionTrackerDto> GetAllTransactions()
        {
            var transactions = transactionsRepository.GetAll()
                                                     .Select(t => mapper.Map<TransactionTrackerDto>(t));
            
            return transactions.ToList();
        }
        public AdminDto CreateAdmin(CreateAdminDto dto)
        {
            var admin = mapper.Map<Admin>(dto);
            admin.Password = Argon2.Hash(dto.Password);
            admin.AuthorizationCode = Argon2.Hash(dto.AuthorizationCode);

            adminRepository.Create(admin);

            return mapper.Map<AdminDto>(dto);
        }
        public AdminDto EditAdmin(EditAdminDto dto, int id)
        {
            var admin = adminRepository.GetById(id);
            if (admin == null)
            {
                throw new Exception("Admin was not found");  
            }

            admin = mapper.Map<Admin>(dto);
            adminRepository.Update(admin);

            return mapper.Map<AdminDto>(dto);
        }
        public void DeleteAdmin(int id)
        {
            var admin = adminRepository.GetById(id);
            if (admin == null)
            {
                throw new Exception("Admin not found");
            }

            adminRepository.Delete(admin);
        }
    }
}