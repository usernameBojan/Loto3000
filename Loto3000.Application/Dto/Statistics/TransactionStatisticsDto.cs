namespace Loto3000.Application.Dto.Statistics
{
    public class TransactionStatisticsDto
    {
        public int TotalTransactions { get; set; }
        public int RegisteredPlayersTransactions { get; set; }
        public int NonregisteredPlayersTransactions { get; set; }
        public int TotalDepositedAmount { get; set; }
        public int RegisteredPlayersDepositedAmount { get; set; }
        public int NonregisteredPlayersDepositedAmount { get; set; }
    }
}