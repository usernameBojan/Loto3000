namespace Loto3000.Domain.Entities
{
    public class Combination : IEntity
    {
        public Combination() { }
        public int Id { get; set; }
        public int Number { get; set; }
        public int TicketId { get; set; }
    }
}