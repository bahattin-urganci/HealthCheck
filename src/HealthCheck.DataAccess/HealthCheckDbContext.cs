using HealthCheck.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace HealthCheck.DataAccess
{
    public class HealthCheckDbContext : IdentityDbContext<ApplicationUser>
    {
        public HealthCheckDbContext(DbContextOptions<HealthCheckDbContext> options)
           : base(options)
        {
        }

        public DbSet<App> Apps { get; set; }
        public DbSet<AppCheckHistory> AppCheckHistories { get; set; }

    }
}
