using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000.Domain.Models
{
    public class Combination
    {
        public Combination() { }
        public IList<Combination> Combinations(int[] nums)
        {
            IList<Combination> combs = new List<Combination>();

            for (int i = 0; i < nums.Length; i++)
            {
                Combination combination = new Combination();

                combination.Id = i;
                if (nums[i] < 1 || nums[i] > 37)
                {
                    throw new Exception("Number is not valid, please choose a number between 1 and 37");
                }
                combination.Number = nums[i];

                combs.Add(combination);
            }

            return combs;
        }
        public int Id { get; set; }
        public int Number { get; set; }
    }
}