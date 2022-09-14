using Loto3000.Application.Dto.Player;
using Loto3000.Application.Dto.PlayerAccountManagment;
using Loto3000.Application.Dto.Transactions;
using Loto3000.Application.Dto.Tickets;
using Loto3000.Application.Repositories;
using Loto3000.Application.Utilities;
using Loto3000.Domain.Entities;
using Loto3000.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using HashidsNet;

namespace Loto3000.Application.Services.Implementation
{
    public class PlayerService : IPlayerService
    {
        private readonly IRepository<User> userRepository;
        private readonly IRepository<Player> playerRepository;
        private readonly IRepository<TransactionTracker> transactionsRepository;
        private readonly IRepository<Draw> drawRepository;
        private readonly IRepository<Ticket> ticketRepository;
        private readonly IEmailSender emailSender;
        private readonly IPasswordHasher hasher;
        private readonly IMapper mapper;
        private readonly IHashids hashids;
        public PlayerService(
            IRepository<User> userRepository,
            IRepository<Player> playerRepository,
            IRepository<TransactionTracker> transactionsRepository,
            IRepository<Draw> drawRepository,
            IRepository<Ticket> ticketRepository,
            IEmailSender emailSender,
            IPasswordHasher hasher,
            IMapper mapper,
            IHashids hashids
            )
        {
            this.userRepository = userRepository;
            this.playerRepository = playerRepository;
            this.transactionsRepository = transactionsRepository;
            this.drawRepository = drawRepository;
            this.ticketRepository = ticketRepository;
            this.emailSender = emailSender;
            this.hasher = hasher;
            this.mapper = mapper;
            this.hashids = hashids;
        }
        public PlayerDto GetPlayer(int id)
        {
            var player = playerRepository.Query()
                                         .Include(x => x.Tickets)
                                         .Include(x => x.TransactionTracker)
                                         .Where(x => x.Id == id)
                                         .FirstOrDefault()
                         ?? throw new NotFoundException("Player not found");

            return mapper.Map<PlayerDto>(player);
        }
        public IEnumerable<PlayerDto> GetPlayers()
        {
            var players = playerRepository.Query()
                                          .Include(x => x.Tickets)
                                          .Include(x => x.TransactionTracker)
                                          .Select(p => mapper.Map<PlayerDto>(p));

            return players.ToList();
        }
        public PlayerDto RegisterPlayer(RegisterPlayerDto dto)
        {
            var users = userRepository.Query();
            var players = playerRepository.Query();

            foreach (var existingUser in users)
            {
                if (dto.Username == existingUser.Username)
                {
                    throw new ValidationException("Username already exists.");
                }
            }

            foreach(var existingPlayer in players)
            {
                if (dto.Email == existingPlayer.Email)
                {
                    throw new ValidationException("This email is connected with another account.");
                };
            }

            var player = mapper.Map<Player>(dto);
            player.Password = hasher.HashToString(dto.Password);
            player.Role = SystemRoles.Player;

            var created = playerRepository.Create(player);

            var code = $"{hashids.Encode(created.Id)}{hashids.Encode(created.FirstName.Length)}";
            created.SetVerificationCode(code);
            playerRepository.Update(created);

            emailSender.SendEmail(EmailContents.RegisterSubject, EmailContents.RegisterBody(created.FirstName, code), created.Email);
            return mapper.Map<PlayerDto>(dto);

            #region DUMMY MAIL
            // use mail - jane.murazik76@ethereal.email TO REGISTER, AND LOG IN on https://ethereal.email/ wtih
            // emial - jane.murazik76@ethereal.email and password - emAuH87yXBcdTHY5Pb to get verification code
            //OR COMMENT -> if(!user.IsVerified) condition in LoginService.cs to login without verification
            #endregion
        }
        public void VerifyPlayer(string code)
        {
            var player = playerRepository.Query().Where(x => x.VerificationCode == code).FirstOrDefault() ?? throw new NotFoundException();

            player.IsVerified = true;
            player.ClearVerificationCode();

            playerRepository.Update(player);
        }
        public void BuyCredits(BuyCreditsDto dto, int id)
        { //REGEX USED https://ihateregex.io/expr/credit-card/ 
            var player = playerRepository.GetById(id) ?? throw new NotFoundException();

            player.BuyCredits(dto.DepositAmount, dto.Credits);

            var transaction = mapper.Map<TransactionTracker>(dto);
            transaction.PlayerName = player.FullName;

            player.TransactionTracker.Add(transaction);

            playerRepository.Update(player);
        }
        public TicketDto CreateTicket(CreateTicketDto dto, int id)
        {
            var player = playerRepository.GetById(id) ?? throw new NotFoundException();

            var draw = drawRepository.Query().WhereActiveDraw().FirstOrDefault() ?? throw new NotFoundException("No draws yet");
            
            var ticket = player.CreateTicket(dto.CombinationNumbers, draw);

            playerRepository.Update(player);

            return mapper.Map<TicketDto>(ticket); 
        }
        public TransactionTrackerDto GetPlayerTransaction(int id, int transactionId)
        {
            var transaction = transactionsRepository.Query()
                                         .Where(t => t.PlayerId == id)
                                         .FirstOrDefault(t => t.Id == transactionId);


            return mapper.Map<TransactionTrackerDto>(transaction);
        }
        public IEnumerable<TransactionTrackerDto> GetPlayerTransactions(int id)
        {
            var transactions = transactionsRepository.Query()
                                                     .Where(t => t.PlayerId == id)
                                                     .Select(t => mapper.Map<TransactionTrackerDto>(t));

            return transactions.ToList();
        }
        public TicketDto GetPlayerTicket(int id, int ticketId)
        {
            var ticket = ticketRepository.Query()
                                         .Where(t => t.PlayerId == id)
                                         .FirstOrDefault(t => t.Id == ticketId);
                                         

            return mapper.Map<TicketDto>(ticket);
        }
        public IEnumerable<TicketDto> GetPlayerTickets(int id)
        {
            var tickets = ticketRepository.Query()
                                          .Where(t => t.PlayerId == id)
                                          .Select(t => mapper.Map<TicketDto>(t));

            return tickets.ToList();
        }
        public void ChangePassword(ChangePasswordDto dto, int id)
        {
            var player = playerRepository.GetById(id) ?? throw new NotFoundException();

            if (dto.OldPassword == dto.Password)
            {
                throw new ValidationException("Old password can not be the same as the new password");
            }

            if(!hasher.Verify(dto.OldPassword, player.Password))
            {
                throw new ValidationException("Old password is wrong");

            }

            player.Password = hasher.HashToString(dto.Password);

            playerRepository.Update(player);
        }
        public void ForgotPassword(ForgotPasswordDto dto)
        {
            var player = playerRepository.Query().FirstOrDefault(x => x.Email == dto.Email) ?? throw new NotFoundException();

            var code = hasher.HashToString(player.Username);

            player.SetForgotPasswordCode(code);
            emailSender.SendEmail(EmailContents.ForgotPasswordSubject, EmailContents.ForgotPasswordBody(code), player.Email);

            #region DUMMY MAIL
            // use mail - jane.murazik76@ethereal.email TO REGISTER, AND LOG IN on https://ethereal.email/ wtih
            // emial - jane.murazik76@ethereal.email and password - emAuH87yXBcdTHY5Pb to get recovery code
            #endregion

            playerRepository.Update(player);
        }
        public void UpdatePasswordByCode(UpdatePasswordDto dto)
        {
            var player = playerRepository.Query().Where(x => x.Username == dto.Username).FirstOrDefault() ?? throw new NotFoundException();

            if (!hasher.Verify(dto.Username, dto.Code)) 
            {
                throw new ValidationException();
            }

            player.ClearForgotPasswordCode();
            player.Password = hasher.HashToString(dto.Password);

            playerRepository.Update(player);
        }
        public void DeletePlayer(int id)
        {
            var player = userRepository.GetById(id) ?? throw new NotFoundException();

            userRepository.Delete(player);
        }
    }
}