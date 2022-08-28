using Loto3000.Domain.Entities;

namespace Loto3000.Domain.Entities
{
    public class SuperAdmin : Admin
    {
        public SuperAdmin() { }
        public SuperAdmin(string firstName, string lastName, string username, string password, string authorizationCode) 
            : base(firstName, lastName, username, password, authorizationCode)
        {
            SuperAdminCredentials = $"Administrator{firstName}{lastName}00009999";
        }
        public string SuperAdminCredentials { get; set; } = string.Empty;
    }
}