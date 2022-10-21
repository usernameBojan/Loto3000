using System.ComponentModel.DataAnnotations;

namespace Loto3000.Application.Dto.PlayerAccountManagment
{
    public class RegisterPlayerDto
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        [RegularExpression(
            "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$",
            ErrorMessage = "Password must contain at least one uppercase letter, " +
            "one lowercase letter, one number, one special symbol and can't be shorter than 8 characters")]
        [Compare("ConfirmPassword", ErrorMessage = "Passwords don't match")]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string ConfirmPassword { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; }
    }
}