﻿namespace Loto3000.Domain.Entities;

public class Ticket : IEntity
{
    public Ticket() { }
    public Ticket(Player player, Draw draw)
    {
        Player = player;
        Draw = draw;
    }
    public void CombinationGenerator(int[] nums)
    {
        IList<Combination> combs = new List<Combination>();

        if(nums.Length < 7)
        {
            throw new Exception("Ticket combination must contain 7 numbers.");
        }

        for (int i = 0; i < nums.Length; i++)
        {
            Combination combination = new Combination();

            combination.Id = i;
            if (nums[i] < 1 || nums[i] > 37)
            {
                throw new Exception("Number is not valid, please choose a number between 1 and 37.");
            }
            combination.Number = nums[i];

            combs.Add(combination);
        }

        CombinationNumbers = combs;
    }
    public int Id { get; set; }
    public IEnumerable<Combination> CombinationNumbers { get; set; } = new List<Combination>();
    public Player? Player { get; set; }
    public Draw? Draw { get; set; }
}