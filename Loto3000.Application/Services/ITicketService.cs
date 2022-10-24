using Loto3000.Application.Dto.Tickets;

namespace Loto3000.Application.Services
{
    public interface ITicketService
    {
        TicketDto GetPlayerTicket(int id, int ticketId);
        TicketDto GetNonregisteredPlayerTicket(int ticketId);
        IEnumerable<TicketDto> GetAllTickets();
        IEnumerable<TicketDto> GetActiveTickets();
        IEnumerable<TicketDto> GetPastTickets();
        IEnumerable<TicketDto> GetPlayerActiveTickets(int id);
        IEnumerable<TicketDto> GetPlayerPastTickets(int id);
        IEnumerable<TicketDto> GetRegisteredPlayersTickets();
        IEnumerable<TicketDto> GetNonregisteredPlayersTickets();
        IEnumerable<TicketDto> GetPlayerTickets(int id);
        TicketDto CreateTicket(CreateTicketDto dto, int id);
        TicketDto CreateTicketNonregisteredPlayer(CreateTicketNonregisteredPlayerDto dto);
    }
}