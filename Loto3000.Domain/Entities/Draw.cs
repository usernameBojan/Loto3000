using Loto3000.Domain.Enums;

namespace Loto3000.Domain.Entities
{
    public class Draw : IEntity
    {
        public Draw() { }
        public Draw(/*int drawOrderNum, */IList<Ticket> tickets, DateTime drawTime, DateTime start, DateTime end)
        {
            //DrawSequenceNumber = drawOrderNum;
            Tickets = tickets;
            DrawTime = drawTime;
            SessionStart = start;
            SessionEnd = end;
        }
        public int Id { get; set; }
        //public int DrawSequenceNumber => Id;
        public IList<DrawNumbers> DrawNumbers { get; private set; } = new List<DrawNumbers>();
        public IList<Ticket> Tickets { get; set; } = new List<Ticket>();    
        public Prizes Prizes { get; set; }
        public DateTime DrawTime { get; set; }
        public DateTime SessionStart { get; set; }
        public DateTime SessionEnd { get; set; }
        //public Session? Session { get; set; }
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
            _ = DateTime.Now.Day == SessionEnd.Day ? DrawNumbers = drawNums : DrawNumbers = new List<DrawNumbers>();
            //_ = DateTime.Now.Day == Session?.End.Day ? DrawNumbers = drawNums : DrawNumbers = new List<DrawNumbers>();
            //DrawNumbers = drawNums;
        }
        public string DrawNumbersString()
        {
            string drawNums = string.Empty;

            if (DrawNumbers.Count == 0)
            {
                drawNums = $"Draw is scheduled for {SessionEnd.Date}.";
            }
            else
            {
                for (int i = 0; i < DrawNumbers.Count; i++)
                {
                    _ = i != DrawNumbers.Count ? drawNums += $"{DrawNumbers[i]}, " : drawNums += $"{DrawNumbers[i]}.";
                }
            };

            return drawNums;
        }
    }
}