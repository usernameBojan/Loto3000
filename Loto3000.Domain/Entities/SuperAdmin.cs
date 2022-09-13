namespace Loto3000.Domain.Entities
{
    public class SuperAdmin : IEntity
    {
        public SuperAdmin() { } 
        public SuperAdmin(int id, string username, string password, string role)
        {
            Id = id;
            Username = username;
            Password = password;
            Role = role;
        }
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;    
    }
}