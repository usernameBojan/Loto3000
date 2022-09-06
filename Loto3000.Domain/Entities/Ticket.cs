using Loto3000.Domain.Enums;

namespace Loto3000.Domain.Entities;

public class Ticket : IEntity
{
    public Ticket() { }
    public Ticket(Player player, Draw draw)
    {
        Player = player;
        Draw = draw;
    }
    
    public int Id { get; set; }
    public string CombinationNumbersString { get; set; } = string.Empty;
    public IList<Combination> CombinationNumbers { get; set; } = new List<Combination>();
    public DateTime TicketCreatedTime { get; set; } = DateTime.Now;
    public Player? Player { get; set; }
    public int PlayerId { get; set; }
    public Draw? Draw { get; set; }
    public Prizes Prize { get; set; }
    public int NumbersGuessed { get; set; }
    public void CombinationGenerator(int[] nums)
    {
        if (nums.Length != 7)
        {
            throw new Exception("Ticket combination must contain 7 numbers.");
        }

        for (int i = 0; i < nums.Length; i++)
        {
            Combination combination = new Combination();

            if (nums[i] < 1 || nums[i] > 37)
            {
                throw new Exception("Number is not valid, please choose a number between 1 and 37.");
            }
            combination.Number = nums[i];

            CombinationNumbers.Add(combination);

            _ = i != nums.Length - 1 ? CombinationNumbersString += $"{nums[i]}, " : CombinationNumbersString += $"{nums[i]}.";
        }
    }
    public void GetPrize(int num)
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