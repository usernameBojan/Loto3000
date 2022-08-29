using System.ComponentModel.DataAnnotations;

namespace Loto3000.Application.Dto.Tickets
{
    public class CreateTicketDto
    {
        [Required]
        public string PlayerName { get; set; } = string.Empty;
        [Required]
        [MinLength(7)]
        [MaxLength(7)]
        public int[] CombinationNumbers { get; set; } = new int[7];
    }
}