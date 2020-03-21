using HealthCheck.DataAccess;
using System;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HealthCheck.Repository;

namespace HealthCheck.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly HealthCheckDbContext _context;
        private IDbContextTransaction _transation;
        private bool _disposed;
        public UnitOfWork(HealthCheckDbContext context)
        {
            _context = context;
            AppRepository = new AppRepository(_context);
            AppCheckHistoryRepository = new AppCheckHistoryRepository(_context);



        }
        public IAppRepository AppRepository { get; private set; }
        public IAppCheckHistoryRepository AppCheckHistoryRepository { get; private set; }


        public bool BeginNewTransaction()
        {

            _transation = _context.Database.BeginTransaction();
            return true;

        }


        public bool RollBackTransaction()
        {

            _transation.Rollback();
            _transation = null;
            return true;

        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            var transaction = _transation ?? _context.Database.BeginTransaction();
            using (transaction)
            {
                try
                {
                    if (_context == null)
                    {
                        throw new ArgumentException("Context is null");
                    }
                    int result = _context.SaveChanges();

                    transaction.Commit();
                    return result;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error on save changes ", ex);
                }
            }
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }
}
