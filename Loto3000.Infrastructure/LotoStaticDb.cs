using Loto3000.Domain.Entities;

namespace Loto3000.Infrastructure
{
    public class LotoStaticDb
    {
        public static IList<Player> Players = new List<Player>
        {
            new Player("john", "smith", "johnny", "dzon123", "john@mail.com", 00.00, new DateTime(1987, 6, 5)){Id=1}
        };

        public static IList<Admin> Admins = new List<Admin>
        {
            new Admin("bob", "bobsky", "bobby", "bob123", "123456789101112"){Id=2},
        };

        public static IList<Draw> Draws = new List<Draw>();
        public static IList<TransactionTracker> Transactions = new List<TransactionTracker>();
        public static SuperAdmin SuperAdmin = new SuperAdmin();
    }
}