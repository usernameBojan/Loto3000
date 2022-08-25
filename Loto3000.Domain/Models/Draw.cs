using Loto3000.Domain.Enums;

namespace Loto3000.Domain.Models
{
    public class Draw : IEntity
    {
        public Draw() { }
        public Draw(int drawOrderNum, IList<Ticket> tickets, DateTime drawTime, Session session)
        {
            DrawSequenceNumber = drawOrderNum;
            Tickets = tickets;
            DrawTime = drawTime;
            Session = session;
        }
        public void DrawNums()
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
            DrawNumbers = drawNums;
        }
        public int Id { get; set; }
        public int DrawSequenceNumber { get; set; }
        public IList<DrawNumbers> DrawNumbers { get; private set; } = new List<DrawNumbers>();
        public IList<Ticket> Tickets { get; set; } = new List<Ticket>();    
        public Prizes Prizes { get; set; }
        public DateTime DrawTime { get; set; }
        public Session? Session { get; set; }
    }
}