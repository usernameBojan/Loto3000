namespace Loto3000.Domain.Entities
{
    public class Draw : IEntity
    {
        private const int minutesAndSeconds = 0;
        private const int hour = 20;
        public Draw() { }
        public Draw(int id, DateTime drawTime, DateTime sessionStart, DateTime sessionEnd)
        {
            Id = id;
            DrawTime = drawTime;
            SessionStart = sessionStart;
            SessionEnd = sessionEnd;
        }
        public Draw(DateTime drawTime, DateTime sessionStart, DateTime sessionEnd)
        {
            DrawTime = drawTime;
            SessionStart = sessionStart;
            SessionEnd = sessionEnd;
        }
        public int Id { get; set; }
        public string DrawNumbersString {get; set; } = string.Empty;
        public IList<DrawNumbers> DrawNumbers { get; private set; } = new List<DrawNumbers>();
        public IList<Ticket> Tickets { get; set; } = new List<Ticket>();    
        public DateTime DrawTime { get; set; }
        public DateTime SessionStart { get; set; }
        public DateTime SessionEnd { get; set; }
        public void DrawNums()
        {
            List<DrawNumbers> drawNums = new();

            Random random = new();
            var winningNums = Enumerable.Range(1, 37)
                                        .OrderBy(x => random.Next())
                                        .Take(8)
                                        .ToList();

            string drawNumsString = string.Empty;

            for (int i = 0; i < 8; i++)
            {
                DrawNumbers drawNumbers = new()
                {
                    Number = winningNums[i]
                };

                drawNums.Add(drawNumbers);

                drawNumsString += i != winningNums.Count - 1 ? $"{winningNums[i]}, " : $"{winningNums[i]}.";
            }

            DrawNumbers = DateTime.Now.Day == DrawTime.Day ? drawNums : new List<DrawNumbers>();
            DrawNumbersString = DrawNumbers.Count == 0 ? $"Draw is scheduled for {SessionEnd.Date.ToString().Substring(0, 10)}." : drawNumsString;
        }
        private static int DaysInMonth(int monthValue)
        {
            int[] thirtyDaysArr = { 4, 6, 9, 11 };
            int[] thirtyOneDaysArr = { 1, 3, 5, 7, 8, 10, 12 };

            if(monthValue != 2)
            {
                for (int i = 0; i < thirtyOneDaysArr.Length; i++)
                {
                    if (monthValue == thirtyOneDaysArr[i])
                        return 31;
                };

                for (int i = 0; i < thirtyDaysArr.Length; i++)
                {
                    if (monthValue == thirtyDaysArr[i]) 
                        return 30;
                };                
            };

            return 28;
        }
        public static Draw SetDrawSession()
        {
            int month = DateTime.Now.Month;
            int monthNext;
            monthNext = month == 12 ? 1 : month + 1;

            int days = DaysInMonth(month);
            int daysNext = DaysInMonth(monthNext);

            int year = DateTime.Now.Year;
            int yearNext;
            yearNext = monthNext == 1 ? year + 1 : year;

            var time = new DateTime(yearNext, monthNext, daysNext, hour, minutesAndSeconds, minutesAndSeconds);
            var start = new DateTime(year, month, days, hour, minutesAndSeconds, minutesAndSeconds);
            var end = new DateTime(yearNext, monthNext, daysNext, hour, minutesAndSeconds, minutesAndSeconds);

            return new(time, start, end);
        }
        public static Draw SetFirstSession()
        {
            int month = DateTime.Now.Month;
            int monthNext;
            monthNext = month == 12 ? 1 : month + 1;

            int day = DateTime.Now.Day;
            int days = DaysInMonth(month);

            int year = DateTime.Now.Year;
            int yearNext;
            yearNext = monthNext == 1 ? year + 1 : year;

            var time = new DateTime(yearNext, month, days, hour, minutesAndSeconds, minutesAndSeconds);
            var start = new DateTime(year, month, day, 0, minutesAndSeconds, minutesAndSeconds);
            var end = new DateTime(yearNext, month, days, hour, minutesAndSeconds, minutesAndSeconds);

            return new(1, time, start, end);
        }
    }
}