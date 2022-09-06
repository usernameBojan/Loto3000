using AutoMapper;
using HashidsNet;
using Loto3000.Application.Dto.Player;
using Loto3000.Application.Dto.PlayerAccountManagment;
using Loto3000.Application.Dto.Transactions;
using Loto3000.Application.Dto.Tickets;
using Loto3000.Application.Repositories;
using Loto3000.Domain.Entities;
using Isopoh.Cryptography.Argon2;

namespace Loto3000.Application.Services.Implementation
{
    public class PlayerService : IPlayerService
    {
        private readonly IRepository<Player> playerRepository;
        private readonly IRepository<TransactionTracker> transactionsRepository;
        private readonly IRepository<Draw> drawRepository;
        private readonly IRepository<Ticket> ticketRepository;
        private readonly IMapper mapper;
        private readonly IHashids hashids;

        public PlayerService(
            IRepository<Player> playerRepository,
            IRepository<TransactionTracker> transactionsRepository,
            IRepository<Draw> drawRepository,
            IRepository<Ticket> ticketRepository,
            IMapper mapper,
            IHashids hashids
            )
        {
            this.playerRepository = playerRepository;
            this.transactionsRepository = transactionsRepository;
            this.drawRepository = drawRepository;
            this.ticketRepository = ticketRepository;
            this.mapper = mapper;
            this.hashids = hashids;
        }
        public PlayerDto GetPlayer(int id)
        {
            var player = playerRepository.GetById(id) ?? throw new Exception("Player not found");

            return mapper.Map<PlayerDto>(player);
        }
        public IEnumerable<PlayerDto> GetPlayers()
        {
            var players = playerRepository.Query().Select(p => mapper.Map<PlayerDto>(p));

            return players.ToList();
        }
        public PlayerDto RegisterPlayer(RegisterPlayerDto dto)
        {
            var players = GetPlayers();

            foreach (var existingPlayer in players)
            {
                if (dto.Username == existingPlayer.Username)
                {
                    throw new Exception("Username already exists.");
                }
                if (dto.Email == existingPlayer.Email)
                {
                    throw new Exception("This email is connected with another account.");
                };
            }

            var player = mapper.Map<Player>(dto);
            player.Password = Argon2.Hash(dto.Password);

            playerRepository.Create(player);

            return mapper.Map<PlayerDto>(dto);
        }
        public void BuyCredits(BuyCreditsDto dto, int id)
        { //REGEX USED https://ihateregex.io/expr/credit-card/ 
            var player = playerRepository.GetById(id) ?? throw new Exception("Player not found");

            player.BuyCredits(dto.DepositAmount, dto.Credits);

            var transaction = mapper.Map<TransactionTracker>(dto);
            transaction.PlayerName = player.FullName;

            player.TransactionTracker.Add(transaction);

            playerRepository.Update(player);
        }
        public TicketDto CreateTicket(CreateTicketDto dto, int id)
        {
            var player = playerRepository.GetById(id) ?? throw new Exception("Player not found.");

            var draw = drawRepository.Query().WhereActiveDraw().FirstOrDefault() ?? throw new Exception("No draws yet.");
            
            var ticket = player.CreateTicket(dto.CombinationNumbers, draw);

            playerRepository.Update(player);

            return mapper.Map<TicketDto>(ticket); 
        }
        public IEnumerable<TransactionTrackerDto> GetPlayerTransactions(int id)
        {
            var transactions = transactionsRepository.Query()
                                                     .Where(t => t.PlayerId == id)
                                                     .Select(t => mapper.Map<TransactionTrackerDto>(t));

            return transactions.ToList();
        }
        public TicketDto GetPlayerTicket(int playerId, int ticketId)
        {
            var ticket = ticketRepository.Query()
                                         .Where(t => t.PlayerId == playerId)
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
                throw new Exception("Old password can not be the same as the new password");
            }

            var player = playerRepository.GetById(id) ?? throw new Exception("Player doesn't exist");

            if (player.Password != Argon2.Hash(dto.OldPassword))
            {
                throw new Exception("Old password is wrong");
            }

            player.Password = Argon2.Hash(dto.Password);

            playerRepository.Update(player);
        }
        public void ForgotPassword(ForgotPasswordDto dto, string url)
        {
            var player = playerRepository.Query().FirstOrDefault(x => x.Email == dto.Email) ?? throw new Exception("Player not found");
            
            var code = hashids.Encode(player.Id);

            //emailsender TO DO

            player.SetForgotPasswordCode(code);

            playerRepository.Update(player);
        }
        public PlayerDto GetPlayerByCode(string code)
        {
            var id = hashids.DecodeSingle(code);

            var player = playerRepository.GetById(id) ?? throw new Exception("Player not found");

            return mapper.Map<PlayerDto>(player);
        }

        public void UpdatePasswordByCode(UpdatePasswordDto dto, string code)
        {
            var id = hashids.DecodeSingle(code);
            if (dto.Id != id)
            {
                throw new Exception("Unauthorized");
            }

            var player = playerRepository.GetById(id) ?? throw new Exception("Player doesn't exist");

            player.ClearForgotPasswordCode();
            player.Password = Argon2.Hash(dto.Password);

            playerRepository.Update(player);
        }
        public void DeletePlayer(int id)
        {
            var player = playerRepository.GetById(id);
            if (player == null)
            {
                throw new Exception("Player not found");
            }

            playerRepository.Delete(player);
        }
    }
}