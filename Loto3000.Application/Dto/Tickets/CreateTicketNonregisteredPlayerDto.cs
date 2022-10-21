using Loto3000.Application.Dto.Transactions;
using System.ComponentModel.DataAnnotations;

namespace Loto3000.Application.Dto.Tickets
{
    public class CreateTicketNonregisteredPlayerDto : BuyCreditsDto
    {
        [Required]
        [Compare("CardHolderName", ErrorMessage = "Name must match cardholder name.")]
        public string Fullname { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(7)]
        [MaxLength(7)]
        public int[] CombinationNumbers { get; set; } = new int[7];
    }
}