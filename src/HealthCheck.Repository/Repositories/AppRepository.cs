using HealthCheck.Domain;
using HealthCheck.Infrastructure.Attributes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCheck.Repository
{
    [Scoped]
    public class AppRepository : Repository<App>,IAppRepository
    {
        public AppRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public Task<List<App>> Apps(string userId) => Query.Where(x => x.UserId == userId).ToListAsync();

        public App GetApp(int id) => Query.Where(x => x.Id == id).Include(x => x.AppCheckHistories).Include(x=>x.User).FirstOrDefault();
    }
}
