namespace Loto3000.Domain.Models
{
    public class Session : IEntity
    {
        public int Id { get; set; }
        public DateTime Start { get; set; } 
        public DateTime End { get; set; }
    }
}
