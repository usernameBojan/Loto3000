namespace Loto3000.Domain.Entities
{
    public class TransactionTracker : IEntity
    {
        public TransactionTracker() { }
        public int Id { get; set; }
        public string PlayerName { get; set; } = string.Empty;
        public double DepositAmount { get; set; }
        public DateTime TransactionDate { get; set; }
        public Player? Player { get; set; }
        public int PlayerId { get; set; }
    }
}