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
        private readonly IMapper mapper;
        private readonly IHashids hashids;

        public PlayerService(
            IRepository<Player> playerRepository, 
            IRepository<TransactionTracker> transactionsRepository, 
            IRepository<Draw> drawRepository, 
            IMapper mapper,
            IHashids hashids
            )
        {
            this.playerRepository = playerRepository;
            this.transactionsRepository = transactionsRepository;
            this.drawRepository = drawRepository;
            this.mapper = mapper;
            this.hashids = hashids;
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
            player.Password = Argon2.Hash(dto.Password);

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
            _ = player.TransactionTracker.Count == 0 ? player.Credits += dto.Credits * 2 : player.Credits += dto.Credits;

            if ((player.TransactionTracker.Count + 1) % 10 == 0)
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
            var transactions = transactionsRepository.GetAll()
                                                     .Where(t => t.PlayerId == id)
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

            //var draw = drawRepository.GetAll().WhereActiveDraw().FirstOrDefault() ?? throw new Exception("No draws yet.");
            var draw = drawRepository.GetById(2) ?? throw new Exception("No draws yet.");
            var ticket = player.CreateTicket(dto.CombinationNumbers, draw);
            playerRepository.Update(player);

            return mapper.Map<TicketDto>(ticket);
        }
        public void ChangePassword(ChangePasswordDto dto, int id)
        {
            if (dto.OldPassword == dto.Password)
            {
                throw new Exception("Old password can not be the same as the new password");
            }

            var player = playerRepository.GetById(id);

            if(player == null)
            {
                throw new Exception("User doesn't exist");
            }

            if (player.Password != Argon2.Hash(dto.OldPassword))
            {
                throw new Exception("Old password is wrong");
            }

            player.Password = Argon2.Hash(dto.Password);

            playerRepository.Update(player);
        }
        public void ForgotPassword(ForgotPasswordDto dto, string url)
        {
            var player = playerRepository.GetAll().FirstOrDefault(x => x.Email == dto.Email);
            if (player == null)
            {
                throw new Exception("Player not found");
            }
            var code = hashids.Encode(player.Id);

            //emailsender TO DO

            player.SetForgotPasswordCode(code);

            playerRepository.Update(player);
        }
        public PlayerDto GetPlayerByCode(string code)
        {
            var id = hashids.DecodeSingle(code);

            var player = playerRepository.GetById(id);

            if (player == null)
            {
                throw new Exception("Player not found");
            }

            return mapper.Map<PlayerDto>(player);
        }

        public void UpdatePasswordByCode(UpdatePasswordDto dto, string code)
        {
            var id = hashids.DecodeSingle(code);
            if (dto.Id != id)
            {
                throw new Exception("Unauthorized");
            }

            var player = playerRepository.GetById(id);

            if (player == null)
            {
                throw new Exception("User doesn't exist");
            }

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