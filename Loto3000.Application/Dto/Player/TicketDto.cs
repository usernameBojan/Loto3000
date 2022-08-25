using Loto3000.Domain.Models;

namespace Loto3000.Application.Dto.Player
{
    public class TicketDto
    {
        public int Id { get; set; }
        public string PlayerName { get; set; } = string.Empty;
        public IList<Combination> CombinationNumbers { get; set; } = new List<Combination>();
        public Draw? Draw { get; set; }
        public Session? Session { get; set; }
    }
}