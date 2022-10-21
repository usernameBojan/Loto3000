namespace Loto3000.Domain.Entities
{
    public class NonregisteredPlayerTransaction : TransactionTracker
    {
        public NonregisteredPlayerTransaction() { }
        public NonregisteredPlayer? NonregisteredPlayer { get; set; }
        public int? NonregisteredPlayerId { get; set; }
    }
}