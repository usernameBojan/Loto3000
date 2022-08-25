using Loto3000.Application.Repositories;
using Loto3000.Domain.Models;

namespace Loto3000.Infrastructure.Repositories
{
    public class TransactionsRepository : IRepository<TransactionTracker>
    {
        public TransactionTracker? GetById(int id)
        {
            return LotoStaticDb.Transactions.FirstOrDefault(x => x.Id == id);
        }
        public IEnumerable<TransactionTracker> GetAll()
        {
            return LotoStaticDb.Transactions;
        }
        public TransactionTracker Create(TransactionTracker entity)
        {
            var id = LotoStaticDb.Transactions.LastOrDefault()?.Id ?? 0;
            entity.Id = ++id;

            LotoStaticDb.Transactions.Add(entity);
            return entity;
        }
        public TransactionTracker Update(TransactionTracker entity)
        {
            throw new Exception("Item can't be edit");
        }
        public TransactionTracker Delete(TransactionTracker entity)
        {
            LotoStaticDb.Transactions.Remove(entity);
            return entity;
        }
    }
}