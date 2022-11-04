using Loto3000.Application.Dto.Draw;
using Loto3000.Domain.Enums;

namespace Loto3000.Application.Dto.Tickets
{
    public class TicketDto
    {
        public int Id { get; set; }
        public string CombinationNumbersString { get; set; } = string.Empty;
        public DateTime TicketCreatedTime { get; set; }
        public Prizes Prize { get; set; }
        public int PlayerId { get; set; }
        public DrawDto? Draw { get; set; }
    }
}