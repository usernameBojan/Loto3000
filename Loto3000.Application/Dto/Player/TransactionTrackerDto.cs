using Loto3000.Domain.Models;

namespace Loto3000.Application.Dto.Player
{
    public class TransactionTrackerDto
    {
        public int Id { get; set; }
        public string PlayerName { get; set; } = string.Empty;
        public double DepositAmount { get; set; }
        public DateTime TransactionDate { get; set; }
        public int PlayerId { get; set; }
    }
}
