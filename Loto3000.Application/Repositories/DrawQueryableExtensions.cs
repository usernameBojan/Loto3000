using Loto3000.Domain.Entities;

namespace Loto3000.Application.Repositories
{
    public static class DrawQueryableExtensions
    {
        public static IEnumerable<Draw> WhereActiveDraw(this IEnumerable<Draw> query)
        {
            var date = DateTime.Now;
            return query.Where(x => x.Session.Start <= date && x.Session.End >= date);
        }
    }
}