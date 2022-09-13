using Loto3000.Application.Dto.Player;
using Loto3000.Application.Dto.PlayerAccountManagment;
using Loto3000.Application.Dto.Transactions;
using Loto3000.Application.Dto.Tickets;

namespace Loto3000.Application.Services
{
    public interface IPlayerService
    {
        //PlayerDto GetPlayer(string username);
        PlayerDto GetPlayer(int id);
        IEnumerable<PlayerDto> GetPlayers();
        PlayerDto RegisterPlayer(RegisterPlayerDto dto);
        void BuyCredits(BuyCreditsDto dto, int id);
        TicketDto CreateTicket(CreateTicketDto dto, int id);
        TransactionTrackerDto GetPlayerTransaction(int id, int transactionId);
        IEnumerable<TransactionTrackerDto> GetPlayerTransactions(int id); 
        IEnumerable<TicketDto> GetPlayerTickets(int id);
        public TicketDto GetPlayerTicket(int id, int ticketId);
        void DeletePlayer(int id);
    }
}