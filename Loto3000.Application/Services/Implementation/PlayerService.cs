using AutoMapper;
using Loto3000.Application.Dto.Player;
using Loto3000.Application.Repositories;
using Loto3000.Domain.Models;

namespace Loto3000.Application.Services.Implementation
{
    public class PlayerService : IPlayerService
    {
        private readonly IRepository<Player> playerRepository;
        private readonly IRepository<TransactionTracker> transactionsRepository;
        private readonly IRepository<Draw> drawRepository;
        private readonly IMapper mapper; 
        public PlayerService(
            IRepository<Player> playerRepository, 
            IRepository<TransactionTracker> transactionsRepository, 
            IRepository<Draw> drawRepository, 
            IMapper mapper
            )
        {
            this.playerRepository = playerRepository;
            this.transactionsRepository = transactionsRepository;
            this.drawRepository = drawRepository;
            this.mapper = mapper;   
        }
        public PlayerDto GetPlayer(int id)
        {
            var player = playerRepository.GetById(id);
            if (player == null)
            {
                throw new Exception("Player not found");
            }

            return mapper.Map<PlayerDto>(player);
        }
        public IEnumerable<PlayerDto> GetPlayers()
        {
            var players = playerRepository.GetAll()
                                          .Select(p => mapper.Map<PlayerDto>(p));

            return players.ToList();
        }
        public PlayerDto RegisterPlayer(RegisterPlayerDto dto)
        {
            var players = GetPlayers();
                
            foreach(var existingPlayer in players)
            {
                if(dto.Username == existingPlayer.Username)
                {
                    throw new Exception("Username already exists.");
                }
                if (dto.Email == existingPlayer.Email)
                {
                    throw new Exception("This email is connected with another account.");
                };
            }

            var player = mapper.Map<Player>(dto);
            playerRepository.Create(player);

            return mapper.Map<PlayerDto>(dto);
        }
        public void BuyCredits(BuyCreditsDto dto, int id)
        {
            var player = playerRepository.GetById(id);

            if (player == null)
            {
                throw new Exception("Player not found");
            }

            if (dto.DepositAmount < 5)
            {
                throw new Exception("Deposited amount must be higher than 5$.");
            }

            #region promo offer
            _ = player.TransactionTracker.Count==0? player.Credits += dto.Credits*2 : player.Credits += dto.Credits;

            if((player.TransactionTracker.Count + 1) % 10 == 0)
            {
                player.Credits += 100;
            }
            #endregion

            var transaction = mapper.Map<TransactionTracker>(dto);
            transaction.PlayerName = player.FullName;

            player.TransactionTracker.Add(transaction);
            transactionsRepository.Create(transaction);

            playerRepository.Update(player);
        }
        public IEnumerable<TransactionTrackerDto> GetPlayerTransactions(int id)
        {
            var player = mapper.Map<PlayerDto>(playerRepository.GetById(id));
            if (player == null)
            {
                throw new Exception("Player not found");
            }

            var transactions = player.TransactionTracker
                                     .Select(t => mapper.Map<TransactionTrackerDto>(t));

            return transactions.ToList();
        }
        public TicketDto CreateTicket(CreateTicketDto dto, int id)
        {
            var player = playerRepository.GetById(id);
            if (player == null)
            {
                throw new Exception("Player not found.");
            }

            var draw = drawRepository.GetAll().WhereActiveDraw().FirstOrDefault();
            var ticket = player.CreateTicket(dto.CombinationNumbers.ToArray(), draw);
            playerRepository.Update(player);

            return mapper.Map<TicketDto>(ticket);
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