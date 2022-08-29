using Loto3000.Application.Dto.Tickets;

namespace Loto3000.Application.Dto.Draw
{
    public class DrawDto
    {
        public int DrawSequenceNumber { get; set; }
        public string DrawNumbersString { get; set; } = string.Empty;
        //public IList<TicketDto>? Tickets {get; set;}
        //public DrawNumbersDto? DrawNumbers { get; set; }
        public DateTime DrawTime { get; set; }
        //public SessionDto? Session { get; set; }
    }
}