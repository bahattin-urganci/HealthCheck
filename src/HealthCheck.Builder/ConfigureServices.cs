using HealthCheck.DataAccess;
using HealthCheck.Infrastructure;
using HealthCheck.Infrastructure.Attributes;
using HealthCheck.Mapper;
using HealthCheck.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Hangfire;
using HealthCheck.Messaging;
using System.Net.Mail;
using System.Net;

namespace HealthCheck.Builder
{
    public static class ConfigureServices
    {
        private static bool InDocker => Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";

        public static IConfiguration Configuration { get; set; }

        public static IServiceCollection AddHealthCheck(this IServiceCollection services)
        {
            AutoMapper.Mapper.Initialize(x => Registrar.Configure(x));

            services.AddHttpContextAccessor();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                    });
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local;
                options.SerializerSettings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat;


            });
            services.AddSingleton(Configuration);
            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection")));
            services.AddHangfireServer();
            services.AddTransient<IMessagingService, MailService>();


            services.AddScoped((serviceProvider) =>
            {
                var config = serviceProvider.GetRequiredService<IConfiguration>();
                return new SmtpClient()
                {
                    Host = config.GetValue<String>("Email:Smtp:Host"),
                    Port = config.GetValue<int>("Email:Smtp:Port"),
                    Credentials = new NetworkCredential(
                            config.GetValue<String>("Email:Smtp:Username"),
                            config.GetValue<String>("Email:Smtp:Password")
                        )
                };
            });
            return services;
        }



        public static IServiceCollection AddHealthCheckDB(this IServiceCollection services)
        {

            services.AddDbContext<HealthCheckDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }


        public static IServiceCollection AddHealthCheckServices(this IServiceCollection services)
        {
            var types = Assembly.LoadFrom(Path.Combine(Constants.AppPath, "HealthCheck.Service.dll")).GetTypes().ToList();
            Add(services, types);
            return services;
        }
        public static IServiceCollection AddHealthCheckRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            return services;
        }

        private static void Add(IServiceCollection services, List<Type> types)
        {
            var classes = types.Where(x => x.CustomAttributes != null);
            foreach (var implementType in classes)
            {
                var interfaceType = implementType.GetInterface("I" + implementType.Name);

                if (interfaceType != null)
                {
                    var toBeAdded = implementType.CustomAttributes.FirstOrDefault(c => c.AttributeType == typeof(ScopedAttribute) || c.AttributeType == typeof(TransientAttribute) || c.AttributeType == typeof(SingletonAttribute));
                    if (toBeAdded != null)
                    {
                        var attribute = toBeAdded.AttributeType.Name.Replace("Attribute", "");
                        services.Add(new ServiceDescriptor(interfaceType, implementType,
                            attribute.ToServiceLifetime()));
                    }
                }

            }
        }

    }
}
