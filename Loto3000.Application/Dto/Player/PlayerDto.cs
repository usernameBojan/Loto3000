using Loto3000.Domain.Models;

namespace Loto3000.Application.Dto.Player
{
    public class PlayerDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public double Credits { get; set; }
        public DateTime DateOfBirth { get; set; }
        public IList<Ticket> Tickets { get; set; } = new List<Ticket>();
        public IList<TransactionTracker> TransactionTracker { get; set; } = new List<TransactionTracker>();
    }
}