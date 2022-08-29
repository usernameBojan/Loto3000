using Loto3000.Application.Dto.Player;
using Loto3000.Application.Dto.PlayerAccountManagment;
using Loto3000.Application.Dto.Transactions;
using Loto3000.Application.Dto.Tickets;

namespace Loto3000.Application.Services
{
    public interface IPlayerService
    {
        PlayerDto GetPlayer(int id);
        IEnumerable<PlayerDto> GetPlayers();
        PlayerDto RegisterPlayer(RegisterPlayerDto dto);
        void BuyCredits(BuyCreditsDto dto, int id);
        IEnumerable<TransactionTrackerDto> GetPlayerTransactions(int id);
        TicketDto CreateTicket(CreateTicketDto dto, int id, int drawId);
        void DeletePlayer(int id);
    }
}