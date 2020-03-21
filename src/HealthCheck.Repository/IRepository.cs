using HealthCheck.Domain;
using HealthCheck.Infrastructure.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCheck.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<C>> SQLQuery<C>(string query, object param = null) where C : class;
        IQueryable<T> Query { get; }
        T FindOne(Expression<Func<T, bool>> expression);
        Task<T> FindOneAsync(Expression<Func<T, bool>> expression);
        IPaginate<T> List(Expression<Func<T, bool>> expression = null, int index = 0, string orderBy = null);
        Task<IPaginate<T>> ListAsync(Expression<Func<T, bool>> expression = null, int index = 0, string orderBy = null);
        void Add(T entity);        
        void AddRange(IEnumerable<T> entities);
        Task AddRangeAsync(IEnumerable<T> entities);
        void Update(T entity);
        void Update(object source, T entity);
        void UpdateRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);

    }
}
