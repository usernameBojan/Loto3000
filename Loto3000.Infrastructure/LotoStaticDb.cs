using Loto3000.Domain.Models;

namespace Loto3000.Infrastructure
{
    public class LotoStaticDb
    {
        public static IList<Player> Players = new List<Player>
        {
            //new Player(){Id = 5, FirstName="dzon", LastName="dzonson", Username="dzony", Password="00009999", Email="dzon@dzon.dzonson",
            //Credits = 00.00, DateOfBirth=new DateOnly(1988, 4, 22), Tickets=new List<Ticket>()}
            new Player("dzon", "dzonson", "dzony", "dzon123", "dzon@dzon.dzon", 00.00, new DateTime(1987, 6, 5), new List<Ticket>()){Id=1}
        };
        public static IList<Admin> Admins = new List<Admin>
        {
            //new Admin(){Id = 10, FirstName="dzon", LastName="dzonson", Password="cool", AuthorizationCode="coolz"}
            new Admin("dzon", "dzonson", "dzony", "dzon123", "dzoko"){Id=2},
        };
        public static SuperAdmin SuperAdmin = new SuperAdmin();
    }
}
