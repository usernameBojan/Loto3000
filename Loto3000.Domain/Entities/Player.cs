using Loto3000.Domain.Exceptions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Loto3000.Domain.Entities
{
    public class Player : User
    {
        private const int TicketPrice = 25;
        private const int MinimumDepositAmount = 5;
        private const int FirstTransactionPromoOffer = 2;
        private const int EachTenthTransactionPromoOffer = 100;
        private bool IsTenthTransaction => (Transactions.Count + 1) % 10 == 0;
        private bool HasTransaction => Transactions.Count != 0;
        private bool HasLessThanTenTransactions => Transactions.Count <= 9;
        public Player() { }
        public string Email { get; set; } = string.Empty;
        public double Credits { get; set; }
        public DateTime DateOfBirth { get; set; }
        public IList<TransactionTracker> Transactions { get; set; } = new List<TransactionTracker>();
        public IList<Ticket> Tickets { get; set; } = new List<Ticket>();
        public string? ForgotPasswordCode { get; private set; }
        public string? VerificationCode { get; private set; }
        public DateTime? ForgotPasswordCodeCreated { get; private set; }
        [NotMapped]
        public int ActiveTickets { get; private set; }
        [NotMapped]
        public int NumberOfPrizesWon { get; private set; }
        public int TransactionsMade => Transactions.Count;
        public int TicketsPlayed => Tickets.Count;
        public double TotalAmountDeposited => !HasTransaction ? 0 : Transactions.Sum(x => x.DepositAmount);
        public int BonusCreditsAwarded => HasLessThanTenTransactions ? 0 : Transactions.Count / 10 * EachTenthTransactionPromoOffer;
        public double HighestAmountDeposited => !HasTransaction ? 0 : Transactions.Max(x => x.DepositAmount);
        public double LowestAmountDeposited => !HasTransaction ? 0 : Transactions.Min(x => x.DepositAmount);
        
        public void GetNumberOfActiveTickets(Draw activeDraw)
        {
            ActiveTickets = Tickets.Where(x => x.Draw!.Id == activeDraw.Id).Count();
        }

        public void GetNumberOfPrizesWon(Draw activeDraw)
        {
            NumberOfPrizesWon = Tickets.Where(x => x.Draw!.Id != activeDraw.Id && (int)x.Prize >= 3).Count();
        }
        
        public void SetVerificationCode(string code)
        {
            VerificationCode = code;
        }

        public void ClearVerificationCode()
        {
            VerificationCode = null;
        }

        public void SetForgotPasswordCode(string code)
        {
            ForgotPasswordCode = code;
            ForgotPasswordCodeCreated = DateTime.Now;
        }

        public void ClearForgotPasswordCode()
        {
            ForgotPasswordCode = null;
            ForgotPasswordCodeCreated = null;
        }

        public void BuyCredits(double deposit, double credits)
        {
            if (deposit < MinimumDepositAmount)
            {
                throw new ValidationException("Deposited amount must be higher than 5$.");
            }

            Credits += Transactions.Count == 0 ? credits * FirstTransactionPromoOffer : credits;

            if (IsTenthTransaction)
            {
                Credits += EachTenthTransactionPromoOffer;
            }
        }

        public Ticket CreateTicket(int[] numbers, Draw draw)
        {
            if (draw == null)
            {
                throw new NotFoundException("There is no active draw.");
            }

            if (Credits < TicketPrice)
            {
                throw new ValidationException("Not enough credits to buy ticket");
            }

            Ticket ticket = new(this, draw);

            Credits -= TicketPrice;

            ticket.CombinationGenerator(numbers);

            Tickets.Add(ticket);

            return ticket;
        }
    }
}