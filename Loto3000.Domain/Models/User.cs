namespace Loto3000.Domain.Models
{
    public class User : IEntity
    {
        public User() { }
        public User(string firstName, string lastName, string username, string pw)
        {
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Password = pw;
        }
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}