using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Internal;
using System;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Hangfire;

namespace HealthCheck.Builder
{
    public static class ApplicationBuilder
    {
        public static IApplicationBuilder UseHealthCheck(this IApplicationBuilder app)
        {  
            
            app.UseRequestResponseLogging();            
            app.UseExceptionHandler();
            app.UseCors("AllowAll");
            app.UseHangfireDashboard();
            //DataAccess.Initializers.BidigoDbInitializer.InitDatabase(app);
            return app;
        }

        

    }
}