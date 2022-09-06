namespace Loto3000.Application.Dto.Transactions
{
    public class TransactionTrackerDto
    {
        public int Id { get; set; }
        public string PlayerName { get; set; }  = string.Empty;
        public double DepositAmount { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}