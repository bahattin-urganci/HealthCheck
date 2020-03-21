using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthCheck.Builder
{
    public static class Extensions
    {
        public static ServiceLifetime ToServiceLifetime(this string attribute)
        {
            switch (attribute)
            {
                case "Transient":
                    return ServiceLifetime.Transient;
                case "Scoped":
                    return ServiceLifetime.Scoped;
                case "Singleton":
                    return ServiceLifetime.Singleton;
                default:
                    return ServiceLifetime.Transient;
            }
        }

        
    }
}
