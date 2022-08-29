using Loto3000.Application.Dto.Draw;
using Loto3000.Domain.Entities;

namespace Loto3000.Application.Dto.Tickets
{
    public class TicketDto
    {
        public int Id { get; set; }
        public string PlayerName { get; set; } = string.Empty;
        //public IEnumerable<Combination> CombinationNumbers { get; set; } = new List<Combination>();
        public DrawDto? Draw { get; set; }
        public SessionDto? Session { get; set; }
    }
}