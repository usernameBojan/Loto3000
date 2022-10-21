using Loto3000.Domain.Entities;

namespace Loto3000.Application.Utilities
{
    internal static class DrawQueryableExtensions
    {
        internal static IQueryable<Draw> WhereActiveDraw(this IQueryable<Draw> query)
        {
            var date = DateTime.Now;
            return query.Where(x => x.SessionStart <= date && x.SessionEnd >= date);
        }
        internal static IQueryable<Draw> WhereConcludedDraw(this IQueryable<Draw> query)
        {
            var date = DateTime.Now;
            return query.Where(x => x.SessionEnd < date);
        }
    }
}