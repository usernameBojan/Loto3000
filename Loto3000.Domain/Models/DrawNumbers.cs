namespace Loto3000.Domain.Models
{
    public class DrawNumbers : IEntity
    {
        public DrawNumbers() { }
        public int Id { get; set; }
        public int Number { get; set; }
    }
}