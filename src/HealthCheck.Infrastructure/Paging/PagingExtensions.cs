using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HealthCheck.Infrastructure.Paging
{
    public static class PaginateExtensions
    {
        public static IPaginate<T> ToPaginate<T>(this IEnumerable<T> source, int index, int size = 10, int from = 0)
        {
            return new Paginate<T>(source, index, size, from);
        }

        public static IPaginate<TResult> ToPaginate<TSource, TResult>(this IEnumerable<TSource> source,
            Func<IEnumerable<TSource>, IEnumerable<TResult>> converter, int index, int size, int from = 0)
        {
            return new Paginate<TSource, TResult>(source, converter, index, size, from);
        }

        public static async Task<IPaginate<T>> ToPaginateAsync<T>(this IQueryable<T> source, int index, int size = 10,
            int from = 0, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (from > index) throw new ArgumentException($"From: {from} > Index: {index}, must From <= Index");

            var count = await source.CountAsync(cancellationToken).ConfigureAwait(false);
            var items = await source.Skip((index - from) * size)
                .Take(size).ToListAsync(cancellationToken).ConfigureAwait(false);

            var list = new Paginate<T>
            {
                Index = index,
                Size = size,
                From = from,
                Count = count,
                Items = items,
                Pages = (int)Math.Ceiling(count / (double)size)
            };

            return list;
        }
    }
}
