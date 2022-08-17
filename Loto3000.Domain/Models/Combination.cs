namespace Loto3000.Domain.Models
{
    public class Combination : IEntity
    {
        public Combination() { }
        public int Id { get; set; }
        public int Number { get; set; }
    }
}