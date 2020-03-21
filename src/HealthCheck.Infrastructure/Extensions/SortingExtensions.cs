using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace HealthCheck.Infrastructure.Extensions
{
    public static class SortingExtensions
    {
        public static IQueryable<T> OrderByPropertyName<T>(this IQueryable<T> q, string sortField)
        {
            if (string.IsNullOrEmpty(sortField))
                return q;
            bool orderByDescending = sortField.StartsWith("-");
            string method = orderByDescending ? "OrderByDescending" : "OrderBy";
            if (orderByDescending)
                sortField = sortField.Replace("-", "");

            var param = Expression.Parameter(typeof(T), "p");
            var prop = Expression.Property(param, sortField);
            var exp = Expression.Lambda(prop, param);
            Type[] types = new Type[] { q.ElementType, exp.Body.Type };
            var rs = Expression.Call(typeof(Queryable), method, types, q.Expression, exp);
            return q.Provider.CreateQuery<T>(rs);
        }
    }
}
