using Dapper;
using HealthCheck.Domain;
using HealthCheck.Infrastructure.Extensions;
using HealthCheck.Infrastructure.Paging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCheck.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
            Query = _dbSet.AsQueryable();
        }


        public T FindOne(Expression<Func<T, bool>> expression) => _dbSet.FirstOrDefault(expression);

        public Task<T> FindOneAsync(Expression<Func<T, bool>> expression) => _dbSet.FirstOrDefaultAsync(expression);
        public IQueryable<T> Query { get; private set; }
        public void Add(T entity) => _dbSet.Add(entity);        
        public void AddRange(IEnumerable<T> entities) => _dbSet.AddRange(entities);
        public Task AddRangeAsync(IEnumerable<T> entities) => _dbSet.AddRangeAsync(entities);
        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
        public void Update(object source, T entity)
        {
            _dbContext.Entry(entity).CurrentValues.SetValues(source);
            _dbSet.Update(entity);
        }
        public void UpdateRange(IEnumerable<T> entities) => _dbSet.UpdateRange(entities);
        public void Remove(T entity) => _dbSet.Remove(entity);
        public void RemoveRange(IEnumerable<T> entities) => _dbSet.RemoveRange(entities);

        public IPaginate<T> List(Expression<Func<T, bool>> expression = null, int index = 0, string orderBy = null)
        {
            Query = expression != null ? Query.Where(expression) : Query;
            if (!string.IsNullOrEmpty(orderBy))
            {
                Query = Query.OrderByPropertyName(orderBy);
            }

            return _dbSet.ToPaginate(index);
        }
        public Task<IPaginate<T>> ListAsync(Expression<Func<T, bool>> expression = null, int index = 0, string orderBy = null)
        {
            Query = expression != null ? Query.Where(expression) : Query;
            if (!string.IsNullOrEmpty(orderBy))
            {
                Query = Query.OrderByPropertyName(orderBy);
            }

            return Query.ToPaginateAsync(index);
        }

        public Task<IEnumerable<C>> SQLQuery<C>(string query, object param = null) where C : class => _dbContext.Database.GetDbConnection().QueryAsync<C>(query, param);

    }
}
