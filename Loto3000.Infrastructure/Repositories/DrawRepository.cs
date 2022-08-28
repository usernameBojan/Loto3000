//using Loto3000.Application.Repositories;
//using Loto3000.Domain.Entities;

//namespace Loto3000.Infrastructure.Repositories
//{
//    public class DrawRepository : IRepository<Draw>
//    {
//        public Draw? GetById(int id)
//        {
//            return LotoStaticDb.Draws.FirstOrDefault(x => x.Id == id);
//        }
//        public IEnumerable<Draw> GetAll()
//        {
//            return LotoStaticDb.Draws;
//        }
//        public Draw Create(Draw entity)
//        {
//            var id = LotoStaticDb.Draws.LastOrDefault()?.Id ?? 0;
//            entity.Id = ++id;

//            LotoStaticDb.Draws.Add(entity);
//            return entity;
//        }
//        public Draw Update(Draw entity)
//        {
//            var draw = GetById(entity.Id);
//            draw = entity;
//            return entity;
//        }
//        public Draw Delete(Draw entity)
//        {
//            LotoStaticDb.Draws.Remove(entity);
//            return entity;
//        }
//    }
//}