using System.ComponentModel.DataAnnotations;

namespace Loto3000.Application.Dto.Player
{
    public class CreateTicketDto
    {
        public int Id { get; set; }
        public string PlayerName { get; set; } = string.Empty;
        [Required]
        [MinLength(7)]
        [MaxLength(7)]
        public IList<int> CombinationNumbers = new List<int>();
    }
}