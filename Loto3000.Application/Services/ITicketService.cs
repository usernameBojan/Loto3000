using Loto3000.Application.Dto.Statistics;
using Loto3000.Application.Dto.Tickets;

namespace Loto3000.Application.Services
{
    public interface ITicketService
    {
        TicketDto GetPlayerTicket(int id, int ticketId);
        TicketDto GetNonregisteredPlayerTicket(int ticketId);
        IEnumerable<TicketDto> GetAllTickets();
        IEnumerable<TicketDto> GetRegisteredPlayersTickets();
        IEnumerable<TicketDto> GetNonregisteredPlayersTickets();
        IEnumerable<TicketDto> GetPlayerTickets(int id);
        TicketStatisticsDto TicketStatistics();
        TicketDto CreateTicket(CreateTicketDto dto, int id);
        TicketDto CreateTicketNonregisteredPlayer(CreateTicketNonregisteredPlayerDto dto);
    }
}