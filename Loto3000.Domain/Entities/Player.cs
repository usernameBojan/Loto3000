﻿namespace Loto3000.Domain.Entities
{
    public class Player : User
    {
        private const int TicketPrice = 25;
        private const int MinimumDepositAmount = 5;
        private const int FirstTransactionPromoOffer = 2;
        private const int EachTenthTransactionPromoOffer = 100;
        private bool IsTenthTransaction => (TransactionTracker.Count + 1) % 10 == 0;
        public Player() { }
        public string Email { get; set; } = string.Empty;
        public double Credits { get; set; }
        public DateTime DateOfBirth { get; set; }
        public IList<TransactionTracker> TransactionTracker { get; set; } = new List<TransactionTracker>();
        public IList<Ticket> Tickets { get; set; } = new List<Ticket>();
        public string? ForgotPasswordCode { get; private set; }
        public DateTime? ForgotPasswordCodeCreated { get; private set; }
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
                throw new Exception("Deposited amount must be higher than 5$.");
            }

            _ = TransactionTracker.Count == 0 ? Credits += credits * FirstTransactionPromoOffer : Credits += credits;

            if (IsTenthTransaction)
            {
                Credits += EachTenthTransactionPromoOffer;
            }
        }
        public Ticket CreateTicket(int[] numbers, Draw draw)
        {
            if (draw == null)
            {
                throw new Exception("There is no active draw.");
            }

            if (Credits < TicketPrice)
            {
                throw new Exception("Not enough credits to buy ticket");
            }

            var ticket = new Ticket(this, draw);
            
            Credits -= TicketPrice;
            ticket.CombinationGenerator(numbers);
            
            Tickets.Add(ticket);

            return ticket;
        }
    }
}