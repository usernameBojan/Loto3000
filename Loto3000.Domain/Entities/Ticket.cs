﻿using Loto3000.Domain.Enums;
using Loto3000.Domain.Exceptions;

namespace Loto3000.Domain.Entities
{
    public class Ticket : IEntity
    {
        private const int AllowedNumbersForTicket = 7;
        private const int LowestNumberValue = 1;
        private const int HighestNumberValue = 37;
        public Ticket() { }
        public Ticket(Player player, Draw draw)
        {
            Player = player;
            Draw = draw;
        }
        public int Id { get; set; }
        public int NumbersGuessed { get; set; }
        public string CombinationNumbersString { get; set; } = string.Empty;
        public IList<Combination> CombinationNumbers { get; set; } = new List<Combination>();
        public DateTime TicketCreatedTime { get; set; } = DateTime.Now;
        public Player? Player { get; set; }
        public int PlayerId { get; set; }
        public Draw? Draw { get; set; }
        public Prizes Prize { get; set; }
        public void CombinationGenerator(int[] nums)
        {
            if (nums.Length != AllowedNumbersForTicket)
            {
                throw new ValidationException("Ticket combination must contain 7 numbers.");
            }

            if (nums.Distinct().Count() != AllowedNumbersForTicket)
            {
                throw new ValidationException("A number can be used only once per combination.");
            }

            for (int i = 0; i < nums.Length; i++)
            {
                Combination combination = new Combination();

                if (nums[i] < LowestNumberValue || nums[i] > HighestNumberValue)
                {
                    throw new ValidationException("Number is not valid, please choose a number between 1 and 37.");
                }
                combination.Number = nums[i];

                CombinationNumbers.Add(combination);

                _ = i != nums.Length - 1 ? CombinationNumbersString += $"{nums[i]}, " : CombinationNumbersString += $"{nums[i]}.";
            }
        }
        public void AssignPrize(int num)
        {
            switch (num)
            {
                case 7:
                    Prize = Prizes.Car;
                    break;
                case 6:
                    Prize = Prizes.Vacation;
                    break;
                case 5:
                    Prize = Prizes.TV;
                    break;
                case 4:
                    Prize = Prizes.GiftCard_100;
                    break;
                case 3:
                    Prize = Prizes.GiftCard_50;
                    break;
                default:
                    Prize = default;
                    break;
            }
        }
    }
}