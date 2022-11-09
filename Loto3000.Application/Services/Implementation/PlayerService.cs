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
using Loto3000.Application.Dto.Statistics;

namespace Loto3000.Application.Services.Implementation
{
    public class PlayerService : IPlayerService
    {
        private readonly IRepository<User> userRepository;
        private readonly IRepository<Player> playerRepository;
        private readonly IRepository<TransactionTracker> transactionsRepository;
        private readonly IRepository<Ticket> ticketRepository;
        private readonly IRepository<Draw> drawRepository;
        private readonly IEmailSender emailSender;
        private readonly IPasswordHasher hasher;
        private readonly IMapper mapper;
        private readonly IHashids hashids;
        public PlayerService(
            IRepository<User> userRepository,
            IRepository<Player> playerRepository,
            IRepository<TransactionTracker> transactionsRepository,
            IRepository<Ticket> ticketRepository,
            IRepository<Draw> drawRepository,
            IEmailSender emailSender,
            IPasswordHasher hasher,
            IMapper mapper,
            IHashids hashids
            )
        {
            this.userRepository = userRepository;
            this.playerRepository = playerRepository;
            this.transactionsRepository = transactionsRepository;
            this.ticketRepository = ticketRepository;
            this.drawRepository = drawRepository;
            this.emailSender = emailSender;
            this.hasher = hasher;
            this.mapper = mapper;
            this.hashids = hashids;
        }
        public PlayerDto GetPlayer(int id)
        {
            var player = playerRepository.Query()
                                         .Include(x => x.Tickets)
                                         .Include(x => x.Transactions)
                                         .Where(x => x.Id == id)
                                         .FirstOrDefault()
                         ?? throw new NotFoundException("Player not found");

            var dto = mapper.Map<PlayerDto>(player);

            dto.Tickets = ticketRepository.Query()
                                          .Where(t => t.PlayerId == id)
                                          .Select(t => mapper.Map<TicketDto>(t));

            dto.TransactionTracker = transactionsRepository.Query()
                                                           .Where(t => t.PlayerId == id)
                                                           .Select(t => mapper.Map<TransactionTrackerDto>(t));
           
            return dto;
        }
        public IEnumerable<PlayerDto> GetPlayers()
        {
            var players = playerRepository.Query()
                                          .Include(x => x.Tickets)
                                          .Include(x => x.Transactions)
                                          .Select(p => mapper.Map<PlayerDto>(p))
                                          .ToList();

            players.ForEach(p => p.TransactionTracker = transactionsRepository.Query().Where(t => t.PlayerId == p.Id).Select(x => mapper.Map<TransactionTrackerDto>(x)));
            players.ForEach(p => p.Tickets = ticketRepository.Query().Where(t => t.PlayerId == p.Id).Select(x => mapper.Map<TicketDto>(x)));

            return players.ToList();
        }
        public PlayerDto RegisterPlayer(RegisterPlayerDto dto)
        {
            if(userRepository.Query().Any(x => x.Username == dto.Username))
            {
                throw new ValidationException("Username already exists.");
            }

            if(playerRepository.Query().Any(x => x.Email == dto.Email))
            {
                throw new ValidationException("This email is connected with another account.");
            }

            if(!IsPlayerLegalAge.VerifyAge(dto.DateOfBirth)) 
            {
                throw new ValidationException("Player must be older than 18 to register.");
            }

            var player = mapper.Map<Player>(dto);
            player.Password = hasher.HashToString(dto.Password);
            player.Role = SystemRoles.Player;

            var created = playerRepository.Create(player);

            var code = $"{hashids.Encode(created.Id)}{hashids.Encode(created.FirstName.Length)}";
            created.SetVerificationCode(code);
            playerRepository.Update(created);

            emailSender.SendEmail(EmailContents.RegisterSubject, EmailContents.RegisterBody(created.FirstName, code), created.Email);
            return mapper.Map<PlayerDto>(created);

            #region DUMMY MAIL
            // use mail - edmond.glover50@ethereal.email TO REGISTER, AND LOG IN on https://ethereal.email/ wtih
            // emial - edmond.glover50@ethereal.email and password - nq8mt3nVU5fT58fKbF to get verification code
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
            // use mail - edmond.glover50@ethereal.email TO REGISTER, AND LOG IN on https://ethereal.email/ wtih
            // email - edmond.glover50@ethereal.email and password - nq8mt3nVU5fT58fKbF to get recovery code
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
        public PlayerTicketsStatisticsDto PlayerTicketsStatistics(int id)
        {
            var activeDraw = drawRepository.Query().WhereActiveDraw().FirstOrDefault() ?? throw new NotFoundException("No active draws");

            var player = playerRepository.Query()
                                         .Include(x => x.Tickets)
                                         .ThenInclude(x => x.Draw)
                                         .Where(x => x.Id == id)
                                         .FirstOrDefault() ?? throw new NotFoundException();

            player.GetNumberOfActiveTickets(activeDraw);
            player.GetNumberOfPrizesWon(activeDraw);

            return mapper.Map<PlayerTicketsStatisticsDto>(player);
        }
        public PlayerTransactionsStatisticsDto PlayerTransactionsStatistics(int id)
        {
            var player = playerRepository.Query().Include(x => x.Transactions).Where(x => x.Id == id).FirstOrDefault() ?? throw new NotFoundException();
            return mapper.Map<PlayerTransactionsStatisticsDto>(player);
        }
        public void DeletePlayer(int id)
        {
            var player = userRepository.GetById(id) ?? throw new NotFoundException();
            var email = playerRepository.Query().Where(x => x.Id == id).FirstOrDefault()!.Email ?? throw new NotFoundException();

            emailSender.SendEmail(EmailContents.SuspendSubject, EmailContents.SuspendBody, email);

            userRepository.Delete(player);
        }
    }
}