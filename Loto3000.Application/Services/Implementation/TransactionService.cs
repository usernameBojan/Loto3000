using AutoMapper;
using Loto3000.Application.Dto.Tickets;
using Loto3000.Application.Dto.Transactions;
using Loto3000.Application.Repositories;
using Loto3000.Domain.Entities;
using Loto3000.Domain.Exceptions;

namespace Loto3000.Application.Services.Implementation
{
    public class TransactionService : ITransactionService
    {
        private readonly IMapper mapper;
        private readonly IRepository<TransactionTracker> transactionsRepository;
        private readonly IRepository<NonregisteredPlayerTransaction> nonregisteredPlayerTransactionRepository;
        private readonly IRepository<Player> playerRepository;

        public TransactionService(IMapper mapper, IRepository<TransactionTracker> transactionsRepository, IRepository<NonregisteredPlayerTransaction> nonregisteredPlayerTransactionRepository, IRepository<Player> playerRepository)
        {
            this.mapper = mapper;
            this.transactionsRepository = transactionsRepository;
            this.nonregisteredPlayerTransactionRepository = nonregisteredPlayerTransactionRepository;
            this.playerRepository = playerRepository;
        }
        public TransactionTrackerDto GetPlayerTransaction(int id, int transactionId)
        {
            var transaction = transactionsRepository.Query()
                                                    .Where(t => t.PlayerId == id)
                                                    .FirstOrDefault(t => t.Id == transactionId);

            return mapper.Map<TransactionTrackerDto>(transaction);
        }
        public TransactionTrackerDto GetNonregisteredPlayerTransaction(int transactionId)
        {
            var transaction = nonregisteredPlayerTransactionRepository.Query().FirstOrDefault(t => t.Id == transactionId);

            return mapper.Map<TransactionTrackerDto>(transaction);
        }
        public IEnumerable<TransactionTrackerDto> GetAllTransactions()
        {
            var transactions = transactionsRepository.Query().Select(t => mapper.Map<TransactionTrackerDto>(t));

            return transactions.ToList();
        }
        public IEnumerable<TransactionTrackerDto> GetPlayerTransactions(int id)
        {
            var transactions = transactionsRepository.Query()
                                                     .Where(t => t.PlayerId == id)
                                                     .Select(t => mapper.Map<TransactionTrackerDto>(t));

            return transactions.ToList();
        }
        public IEnumerable<TransactionTrackerDto> GetRegisteredPlayersTransactions()
        {
            var transactions = transactionsRepository.Query()
                                                     .Where(x => x.PlayerId != null)
                                                     .Select(t => mapper.Map<TransactionTrackerDto>(t));

            return transactions.ToList();
        }
        public IEnumerable<TransactionTrackerDto> GetNonregisteredPlayersTransactions()
        {
            var transactions = nonregisteredPlayerTransactionRepository.Query().Select(t => mapper.Map<TransactionTrackerDto>(t));

            return transactions.ToList();
        }
        public void BuyCredits(BuyCreditsDto dto, int id)
        { //REGEX USED https://ihateregex.io/expr/credit-card/ 
            var player = playerRepository.GetById(id) ?? throw new NotFoundException();

            player.BuyCredits(dto.DepositAmount, dto.Credits);

            var transaction = mapper.Map<TransactionTracker>(dto);
            transaction.PlayerName = player.FullName;

            player.Transactions.Add(transaction);

            playerRepository.Update(player);
        }
    }
}