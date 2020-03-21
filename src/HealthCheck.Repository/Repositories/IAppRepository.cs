using HealthCheck.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthCheck.Repository
{
    public interface IAppRepository : IRepository<App>
    {
        Task<List<App>> Apps(string userId);

        App GetApp(int id);
    }
}
