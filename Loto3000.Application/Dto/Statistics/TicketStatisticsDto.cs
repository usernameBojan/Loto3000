namespace Loto3000.Application.Dto.Statistics
{
    public class TicketStatisticsDto
    {
        public int TotalTickets { get; set; }
        public int ActiveTickets { get; set; }
        public int RegisteredPlayersTickets { get; set; }
        public int NonregisteredPlayersTickets { get; set; }
        public int TotalPrizesWon { get; set; }
    }
}