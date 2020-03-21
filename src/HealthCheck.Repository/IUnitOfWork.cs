using HealthCheck.Repository;
using System;
using System.Threading.Tasks;

namespace HealthCheck.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IAppRepository AppRepository { get; }
        IAppCheckHistoryRepository AppCheckHistoryRepository { get; }
        bool BeginNewTransaction();
        bool RollBackTransaction();
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
