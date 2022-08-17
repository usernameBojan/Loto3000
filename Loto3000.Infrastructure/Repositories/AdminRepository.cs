using Loto3000.Application.Repositories;
using Loto3000.Domain.Models;

namespace Loto3000.Infrastructure.Repositories
{
    public class AdminRepository : IRepository<Admin>
    {
        public Admin? GetById(int id)
        {
            return LotoStaticDb.Admins.FirstOrDefault(x => x.Id == id); 
        }
        public List<Admin> GetAll()
        {
            return LotoStaticDb.Admins.ToList();    
        }
        public Admin Create(Admin entity)
        {
            var id = LotoStaticDb.Admins.LastOrDefault()?.Id ?? 0;
            entity.Id = ++id;

            LotoStaticDb.Admins.Add(entity);
            return entity;
        }
        public Admin Update(Admin entity)
        {
            var player = GetById(entity.Id);
            player = entity;
            return entity;
        }
        public Admin Delete(Admin entity)
        {
            LotoStaticDb.Admins.Remove(entity);
            return entity;
        }             
    }
}
