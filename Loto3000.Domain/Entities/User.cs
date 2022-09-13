namespace Loto3000.Domain.Entities
{
    public class User : IEntity
    {
        public User() { }
        public User(string firstName, string lastName, string username, string pw, string role)
        {
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Password = pw;
            Role = role;
        }
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}