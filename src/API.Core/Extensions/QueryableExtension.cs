using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace API.Core.Extensions
{
    public static class QueryableExtension
    {
        public static IQueryable<T> AsTracked<T>(this IQueryable<T> source, bool isTracked = false) where T : class
        {
            return isTracked ? source.AsTracking() : source.AsNoTracking();
        }
    }
}
