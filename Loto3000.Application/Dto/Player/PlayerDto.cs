using Loto3000.Application.Dto.Tickets;
using Loto3000.Application.Dto.Transactions;
using Loto3000.Application.Utilities;
using System.Text.Json.Serialization;

namespace Loto3000.Application.Dto.Player
{
    public class PlayerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public double Credits { get; set; }
        public DateTime DateOfBirth { get; set; }
        public IEnumerable<TicketDto> Tickets { get; set; } = new List<TicketDto>();
        public IEnumerable<TransactionTrackerDto> TransactionTracker { get; set; } = new List<TransactionTrackerDto>();
    }
}