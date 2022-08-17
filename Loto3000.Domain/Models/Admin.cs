namespace Loto3000.Domain.Models
{
    public class Admin : User
    {
        public Admin() { }
        public Admin(string firstName, string lastName, string username, string password, string authorizationCode) : base (firstName, lastName, username, password)
        {
            AdminCredentials = $"Admin{firstName}";
            AuthorizationCode = authorizationCode;
        }
        public string AdminCredentials { get; set; } = string.Empty;
        public string AuthorizationCode { get; set; } = string.Empty;
    }
}
