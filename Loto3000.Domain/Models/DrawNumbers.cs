using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000.Domain.Models
{
    public class DrawNumbers
    {
        public DrawNumbers() { }
        public IList<DrawNumbers> DrawNums()
        {
            List<DrawNumbers> drawNums = new List<DrawNumbers>();

            Random random = new Random();
            var winningNums = Enumerable.Range(1, 37)
                                .OrderBy(x => random.Next())
                                .Take(8)
                                .ToList();

            for (int i = 0; i < 8; i++)
            {
                DrawNumbers drawNumbers = new DrawNumbers();

                drawNumbers.Id = i;
                drawNumbers.Number = winningNums[i];
                
                drawNums.Add(drawNumbers);
            }

            return drawNums;
        }
        public int Id { get; set; }
        public int Number { get; set; }
    }
}