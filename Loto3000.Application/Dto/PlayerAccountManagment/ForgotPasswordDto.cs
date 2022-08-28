using System.ComponentModel.DataAnnotations;

namespace Loto3000.Application.Dto.PlayerAccountManagment
{
    public class ForgotPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}