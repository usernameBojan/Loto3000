using Loto3000.Domain.Entities;

namespace Loto3000.Application.Utilities
{
    public static class CombineUsersForRegisterAndLogin
    {
        public static IEnumerable<User> Combine(IQueryable<Player> players, IQueryable<Admin> admins)
        {
            var users = new List<User>();

            foreach(var player in players)
            {
                users.Add(player);
            }

            foreach (var admin in admins)
            {
                users.Add(admin);
            }

            return users;
        }
    }
}