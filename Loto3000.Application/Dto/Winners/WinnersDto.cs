using Loto3000.Domain.Enums;

namespace Loto3000.Application.Dto.Winners
{
    public class WinnersDto
    {
        public string PlayerName { get; set; } = string.Empty;
        public string CombinationNumbersString { get; set; } = string.Empty;
        public Prizes Prize { get; set; }
    }
}