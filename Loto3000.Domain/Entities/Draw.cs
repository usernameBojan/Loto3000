using Loto3000.Domain.Enums;

namespace Loto3000.Domain.Entities
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
        public int Id { get; set; }
        public int DrawSequenceNumber { get; set; }
        public IList<DrawNumbers> DrawNumbers { get; private set; } = new List<DrawNumbers>();
        public IList<Ticket> Tickets { get; set; } = new List<Ticket>();    
        public Prizes Prizes { get; set; }
        public DateTime DrawTime { get; set; }
        public Session? Session { get; set; }
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
            _ = DateTime.Now.Day == Session?.End.Day ? DrawNumbers = drawNums : DrawNumbers = new List<DrawNumbers>();
            //DrawNumbers = drawNums;
        }
        public string DrawNumbersString()
        {
            string drawNums = string.Empty;

            if (DrawNumbers.Count == 0)
            {
                drawNums = $"Draw is scheduled for {Session?.End.Date}.";
            }
            else
            {
                for (int i = 0; i < DrawNumbers.Count; i++)
                {
                    _ = i != DrawNumbers.Count - 1 ? drawNums += $"{DrawNumbers[i]}, " : drawNums += $"{DrawNumbers[i]}.";
                }
            };

            return drawNums;
        }
    }
}