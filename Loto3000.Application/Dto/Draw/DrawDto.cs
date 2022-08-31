using Loto3000.Application.Dto.Tickets;

namespace Loto3000.Application.Dto.Draw
{
    public class DrawDto
    {
        //public int DrawSequenceNumber { get; set; }
        public string DrawNumbersString { get; set; } = string.Empty;
        public DateTime DrawTime { get; set; }
        public DateTime SessionStart { get; set; }
        public DateTime SessionEnd { get; set; }

    }
}