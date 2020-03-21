using HealthCheck.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthCheck.Repository
{
    public class AppCheckHistoryRepository : Repository<AppCheckHistory>, IAppCheckHistoryRepository
    {
        public AppCheckHistoryRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
