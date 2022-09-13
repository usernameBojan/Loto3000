using System.ComponentModel.DataAnnotations;

namespace Loto3000.Application.Dto.Admin
{
    public class CreateAdminDto
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        [RegularExpression(
            "^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{8,}$", 
            ErrorMessage = 
            "Password must contain at least one uppercase letter, one lowercase letter, one number and can't be shorter than 8 characters.")]
        public string Password { get; set; } = string.Empty; 
    }
}