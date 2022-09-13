using AutoMapper;
using Loto3000.Application.Dto.Admin;
using Loto3000.Application.Dto.Transactions;
using Loto3000.Application.Repositories;
using Loto3000.Application.Utilities;
using Loto3000.Domain.Entities;
using Loto3000.Application.Dto.Tickets;
using Loto3000.Domain.Exceptions;

namespace Loto3000.Application.Services.Implementation
{
    public class AdminService : IAdminService
    {
        private readonly IRepository<User> userRepository;
        private readonly IRepository<Admin> adminRepository;
        private readonly IRepository<TransactionTracker> transactionsRepository;
        private readonly IRepository<Ticket> ticketRepository;
        private readonly IPasswordHasher passwordHasher;
        private readonly IMapper mapper;
        public AdminService(
            IRepository<User> userRepository,
            IRepository<Admin> adminRepository,
            IRepository<TransactionTracker> transactionsRepository,
            IRepository<Ticket> ticketRepository,
            IPasswordHasher passwordHasher,
            IMapper mapper
)
        {
            this.adminRepository = adminRepository;
            this.userRepository = userRepository;
            this.transactionsRepository = transactionsRepository;
            this.ticketRepository = ticketRepository;
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

            var users = userRepository.Query();

            foreach(var user in users)
            {
                if(user.Username == dto.Username)
                {
                    throw new ValidationException("Username already exists");
                }
            }

            admin.Password = passwordHasher.HashToString(dto.Password);
            admin.Role = SystemRoles.Administrator;

            adminRepository.Create(admin);

            return mapper.Map<AdminDto>(dto);
        }
        public void DeleteAdmin(int id)
        {
            var admin = adminRepository.GetById(id) ?? throw new NotFoundException();

            adminRepository.Delete(admin);
        }
    }
}