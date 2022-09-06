using AutoMapper;
using Loto3000.Application.Dto.Admin;
using Loto3000.Application.Dto.Transactions;
using Loto3000.Application.Repositories;
using Loto3000.Domain.Entities;
using Isopoh.Cryptography.Argon2;
using Loto3000.Application.Dto.Tickets;

namespace Loto3000.Application.Services.Implementation
{
    public class AdminService : IAdminService
    {
        private readonly IRepository<Admin> adminRepository;
        private readonly IRepository<TransactionTracker> transactionsRepository;
        private readonly IRepository<Ticket> ticketRepository;
        private readonly IMapper mapper;
        public AdminService(
            IRepository<Admin> adminRepository, 
            IRepository<TransactionTracker> transactionsRepository, 
            IRepository<Ticket> ticketRepository, 
            IMapper mapper
            )
        {
            this.adminRepository = adminRepository;
            this.transactionsRepository = transactionsRepository;
            this.mapper = mapper;
            this.ticketRepository = ticketRepository;
        }
        public AdminDto GetAdmin(int id)
        {
            var admin = adminRepository.GetById(id) ?? throw new Exception("Admin not found");
           
            return mapper.Map<AdminDto>(admin);
        }
        public IEnumerable<AdminDto> GetAdmins()
        {
            var admins = adminRepository.Query().Select(a => mapper.Map<AdminDto>(a));

            return admins.ToList();
        }
        public IEnumerable<TransactionTrackerDto> GetAllTransactions()
        {
            var transactions = transactionsRepository.Query().Select(t => mapper.Map<TransactionTrackerDto>(t));
            
            return transactions.ToList();
        }
        public IEnumerable<TicketDto> GetAllTickets()
        {
            var tickets = ticketRepository.Query().Select(t => mapper.Map<TicketDto>(t));

            return tickets.ToList();
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
            var admin = adminRepository.GetById(id) ?? throw new Exception("Admin was not found");
            
            admin = mapper.Map<Admin>(dto);
            adminRepository.Update(admin);

            return mapper.Map<AdminDto>(dto);
        }
        public void DeleteAdmin(int id)
        {
            var admin = adminRepository.GetById(id) ?? throw new Exception("Admin was not found");

            adminRepository.Delete(admin);
        }
    }
}