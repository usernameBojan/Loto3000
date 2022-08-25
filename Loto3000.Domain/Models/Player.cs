namespace Loto3000.Domain.Models
{
    public class Player : User
    {
        private const int TicketPrice = 25;
        public Player() { }
        public Player
            (string firstName, string lastName, string username, string password, string email, double credits, DateTime dateOfBirth) 
            : base(firstName, lastName, username, password)
        {
            Email = email;
            Credits = credits;
            DateOfBirth = dateOfBirth;
        }
        public Ticket CreateTicket(int[] numbers, Draw draw)
        {
            if(draw == null)
            {
                throw new Exception("There is no active draw.");
            }
            if(this.Credits < TicketPrice)
            {
                throw new Exception("Not enough credits to buy ticket");
            }
            var ticket = new Ticket(this, draw);
            this.Credits -= TicketPrice;
            ticket.CombinationGenerator(numbers);

            Tickets.Add(ticket);

            return ticket;
        }
        public string Email { get; set; } = string.Empty;
        public double Credits { get; set; }
        public DateTime DateOfBirth { get; set; }
        public IList<TransactionTracker> TransactionTracker { get; set; } = new List<TransactionTracker>();
        public IList<Ticket> Tickets { get; set; } = new List<Ticket>();    
    }
}