using Loto3000.Domain.Enums;

namespace Loto3000.Application.Dto.Winners
{
    public class WinnersDto
    {
        public WinnersDto(string playerName, string combinationNumbersString, Prizes prize)
        {
            PlayerName = playerName;
            CombinationNumbersString = combinationNumbersString;
            Prize = prize;
        }
        public string PlayerName { get; private set; } = string.Empty;
        public string CombinationNumbersString { get; private set; } = string.Empty;
        public Prizes Prize { get; private set; }
    }
}