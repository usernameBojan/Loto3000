using Loto3000.Application.Dto.Statistics;
using Loto3000.Application.Dto.Transactions;

namespace Loto3000.Application.Services
{
    public interface ITransactionService
    {
        TransactionTrackerDto GetPlayerTransaction(int id, int transactionId);
        TransactionTrackerDto GetNonregisteredPlayerTransaction(int transactionId);
        IEnumerable<TransactionTrackerDto> GetAllTransactions();
        IEnumerable<TransactionTrackerDto> GetPlayerTransactions(int id);
        IEnumerable<TransactionTrackerDto> GetRegisteredPlayersTransactions();
        IEnumerable<TransactionTrackerDto> GetNonregisteredPlayersTransactions();
        TransactionStatisticsDto TransactionStatistics();
        void BuyCredits(BuyCreditsDto dto, int id);
    }
}
