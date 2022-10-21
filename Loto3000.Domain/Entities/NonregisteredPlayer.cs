using Loto3000.Domain.Exceptions;

namespace Loto3000.Domain.Entities
{
    public class NonregisteredPlayer : IEntity
    {
        public NonregisteredPlayer() { }
        public NonregisteredPlayer(string fullName, string email, int depositAmount)
        {
            FullName = fullName;
            Email = email;
            DepositAmount = depositAmount;
        }

        private const int NonregistertedPlayerTicketPrice = 5;
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int DepositAmount { get; set; }
        public NonregisteredPlayerTicket? Ticket { get; set; }
        public NonregisteredPlayerTransaction? Transaction { get; set; }
        public NonregisteredPlayerTicket CreateTicket(int[] nums, Draw draw)
        {
            if (draw == null)
            {
                throw new NotFoundException("There is no active draw.");
            }

            if (DepositAmount != NonregistertedPlayerTicketPrice)
            {
                throw new NotAllowedException("Ticket price for non-registered player is 5.");
            }

            Ticket = new(this, draw);
            Ticket.CombinationGenerator(nums);

            Transaction = new()
            {
                NonregisteredPlayer = this,
                PlayerName = FullName,
                DepositAmount = DepositAmount,
                TransactionDate = DateTime.Now
            };

            return Ticket;
        }
    }
}