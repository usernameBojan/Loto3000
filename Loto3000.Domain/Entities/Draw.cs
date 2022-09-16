namespace Loto3000.Domain.Entities
{
    public class Draw : IEntity
    {
        public Draw() { }
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
                DrawNumbers drawNumbers = new();

                drawNumbers.Number = winningNums[i];

                drawNums.Add(drawNumbers);

                _ = i != winningNums.Count - 1 ? drawNumsString += $"{winningNums[i]}, " : drawNumsString += $"{winningNums[i]}.";
            }
            _ = DateTime.Now.Day == DrawTime.Day? DrawNumbers = drawNums : DrawNumbers = new List<DrawNumbers>();
            _ = DrawNumbers.Count == 0 ? DrawNumbersString = $"Draw is scheduled for {SessionEnd.Date.ToString().Substring(0, 10)}." : DrawNumbersString = drawNumsString;
        }
        //GUIDELINES FOR TESTING
        //MAKE SURE THERE ARE NO DRAWS IN DATABASE
        //COMMENT THE FIRST IF STATEMENT IN InitiateDraw() IN DrawService.cs WHICH CHECKS IF DRAW DATE IS EQUAL TO TODAYS DATE
        // CHANGE THE DrawTime.Day CONDITION IN _ = DateTime.Now.Day == DrawTime.Day? IN DrawNums() METHOD (line 35) WITH THE NUMBER VALUE OF TODAYS DATE
        //FIRST, DECOMMENT THE COMMENTED CODE BLOCK IN SetDrawSession() AND COMMENT THE CODE BLOCK BELLOW
        //ACTIVATE DRAW WITH ActivateFirstDraw ACTION IN DrawController.cs WITH SWAGGER OR POSTMAN (FRONT-END IMPLEMENTATION TO DO)
        //RETURN TO Draw.cs
        //DECOMMENT THE ORIGINAL(LOWER) CODE BLOCK IN SetDrawSession() AND COMMENT THE TESTING ONE(UPPER)
        //INITIATE DRAW WITH InitiateDraw ACTION IN DrawController.cs WITH SWAGGER OR POSTMAN (FRONT-END IMPLEMENTATION TO DO)
        private static int DaysInMonth(int monthValue)
        {
            int[] thirtyDaysArr = { 4, 6, 9, 11 };
            int[] thirtyOneDaysArr = { 1, 3, 5, 7, 8, 10, 12 };

            if(monthValue != 2)
            {
                for (int i = 0; i < thirtyDaysArr.Length; i++)
                {
                    if (monthValue == thirtyDaysArr[i]) 
                        return 30;
                };

                for (int i = 0; i < thirtyOneDaysArr.Length; i++)
                {
                    if (monthValue == thirtyOneDaysArr[i]) 
                        return 31;
                };
            };

            return 28;
        }
        public void SetDrawSession()
        {
            const int minutesAndSeconds = 0;
            const int hour = 20;

            int month = DateTime.Now.Month;
            int monthNext;
            _ = month == 12 ? monthNext = 1 : monthNext = month + 1;

            int days = DaysInMonth(month);
            int daysNext = DaysInMonth(monthNext);

            int year = DateTime.Now.Year;
            int yearNext;
            _ = monthNext == 1 ? yearNext = year + 1 : yearNext = year;

            #region FOR TESTING
            //DrawTime = new DateTime(yearNext, monthNext - 1, daysNext - 1, hour, minutesAndSeconds, minutesAndSeconds);
            //SessionStart = new DateTime(year, month - 1, days - 1, hour, minutesAndSeconds, minutesAndSeconds);
            //SessionEnd = new DateTime(yearNext, monthNext - 1, daysNext - 1, hour, minutesAndSeconds, minutesAndSeconds);
            #endregion

            #region ORIGINAL
            DrawTime = new DateTime(yearNext, monthNext, daysNext, hour, minutesAndSeconds, minutesAndSeconds);
            SessionStart = new DateTime(year, month, days, hour, minutesAndSeconds, minutesAndSeconds);
            SessionEnd = new DateTime(yearNext, monthNext, daysNext, hour, minutesAndSeconds, minutesAndSeconds);
            #endregion
        }
    }
}