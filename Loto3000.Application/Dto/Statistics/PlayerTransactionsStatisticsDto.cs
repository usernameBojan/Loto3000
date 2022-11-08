namespace Loto3000.Application.Dto.Statistics
{
    public class PlayerTransactionsStatisticsDto
    {
        public double Credits { get; set; }
        public int TransactionsMade { get; set; }
        public double TotalAmountDeposited { get; set; }
        public int BonusCreditsAwarded { get; set; }
        public double HighestAmountDeposited { get; set; }
        public double LowestAmountDeposited { get; set; }
    }
}