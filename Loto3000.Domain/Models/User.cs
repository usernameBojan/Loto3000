using Loto3000.Domain.Enums;

namespace Loto3000.Domain.Models
{
    public class User
    {
        public User() { }
        public User(int id, string firstName, string lastName, string username, string pw, string email, double credits, DateOnly dateOfBirth, Role role, IList<Ticket> tickets)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Password = pw;
            Email = email;
            Credits = credits;
            Role = role;
            DateOfBirth = dateOfBirth;
            Tickets = tickets;
        }
        public int Id { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string Username { get; set; }
        public string Password { get; set; }    
        public string Email { get; set; }
        public double Credits { get; set; }
        public Role Role { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public IList<Ticket> Tickets { get; set; }
    }
}