using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000.Domain.Models
{
    public class SuperAdmin : Admin
    {
        public SuperAdmin() { }
        public SuperAdmin(string firstName, string lastName, string username, string password, string authorizationCode) 
            : base(firstName, lastName, username, password, authorizationCode)
        {
            SuperAdminCredentials = "Administrator";
        }

        public string SuperAdminCredentials { get; set; } = string.Empty;

    }
}
