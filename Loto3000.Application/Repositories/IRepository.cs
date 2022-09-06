namespace Loto3000.Application.Repositories
{
    public interface IRepository<T>
    {
        T? GetById(int id);
        IQueryable<T> Query();
        T Create(T entity);
        T Update(T entity);
        T Delete(T entity);
    }
}