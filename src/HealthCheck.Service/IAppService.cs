using HealthCheck.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthCheck.Service
{
    public interface IAppService
    {
        Task<List<AppDTO>> Apps();
        AppDTO App(int id);
        Task<AppDTO> SaveApp(AppDTO app);
        Task<int> RemoveApp(int id);
    }
}
