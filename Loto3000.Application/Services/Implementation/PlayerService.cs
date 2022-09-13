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
        private readonly IPasswordHasher passwordHasher;
        private readonly IMapper mapper;
        private readonly IHashids hashids;
        public PlayerService(
            IRepository<Player> playerRepository,
            IRepository<TransactionTracker> transactionsRepository,
            IRepository<Draw> drawRepository,
            IRepository<Ticket> ticketRepository,
            IPasswordHasher passwordHasher,
            IMapper mapper,
            IHashids hashids
            )
        {
            this.playerRepository = playerRepository;
            this.transactionsRepository = transactionsRepository;
            this.drawRepository = drawRepository;
            this.ticketRepository = ticketRepository;
            this.passwordHasher = passwordHasher;
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
            player.Password = passwordHasher.HashToString(dto.Password);
            player.Role = SystemRoles.Player;

            playerRepository.Create(player);

            return mapper.Map<PlayerDto>(dto);
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
            if (dto.OldPassword == dto.Password)
            {
                throw new ValidationException("Old password can not be the same as the new password");
            }

            var player = playerRepository.GetById(id) ?? throw new NotFoundException();

            if (player.Password != passwordHasher.HashToString(dto.OldPassword))
            {
                throw new ValidationException("Old password is wrong");
            }

            player.Password = passwordHasher.HashToString(dto.OldPassword);

            playerRepository.Update(player);
        }
        public void ForgotPassword(ForgotPasswordDto dto, string url)
        {
            var player = playerRepository.Query().FirstOrDefault(x => x.Email == dto.Email) ?? throw new NotFoundException();

            var code = hashids.Encode(player.Id);

            //emailsender TO DO

            player.SetForgotPasswordCode(code);

            playerRepository.Update(player);
        }

        //public PlayerDto GetPlayerByCode(string code)
        //{
        //    var id = hashids.DecodeSingle(code);

        //    var player = playerRepository.GetById(id) ?? throw new NotFoundException();

        //    return mapper.Map<PlayerDto>(player);
        //}
        //TO DO
        //public void UpdatePasswordByCode(UpdatePasswordDto dto, string code)
        //{
        //    var id = hashids.DecodeSingle(code);
        //    if (dto.Id != id)
        //    {
        //        throw new NotAllowedException();
        //    }

        //    var player = playerRepository.GetById(id) ?? throw new NotFoundException();

        //    player.ClearForgotPasswordCode();
        //    player.Password = passwordHasher.HashToString(dto.Password);

        //    playerRepository.Update(player);
        //}
        public void DeletePlayer(int id)
        {
            var player = playerRepository.GetById(id) ?? throw new NotFoundException();

            playerRepository.Delete(player);
        }
    }
}