using Loto3000.Application.Repositories;
using Loto3000.Domain.Models;

namespace Loto3000.Infrastructure.Repositories
{
    public class PlayerRepository : IRepository<Player>
    {
        public Player? GetById(int id)
        {
            return LotoStaticDb.Players.FirstOrDefault(x => x.Id == id);
        }
        public IEnumerable<Player> GetAll()
        {
            return LotoStaticDb.Players;
        }
        public Player Create(Player entity)
        {
            var id = LotoStaticDb.Players.LastOrDefault()?.Id ?? 0;
            entity.Id = ++id;

            LotoStaticDb.Players.Add(entity);
            return entity;
        }
        public Player Update(Player entity)
        {
            var player = GetById(entity.Id);
            player = entity;
            return entity;
        }
        public Player Delete(Player entity)
        {
            LotoStaticDb.Players.Remove(entity);
            return entity;
        }           
    }
}
