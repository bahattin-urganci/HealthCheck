using AutoMapper;
using HealthCheck.Infrastructure;
using HealthCheck.Infrastructure.Attributes;
using HealthCheck.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace HealthCheck.Mapper
{
    public class Registrar : Profile
    {


        public static void Configure(IMapperConfigurationExpression cfg)
        {
            string codeBase = Constants.AppPath;

            var domainAssembly = Assembly.LoadFrom(codeBase + "\\HealthCheck.Domain.dll")
                                         .GetTypes()
                                         .Where(x => x.CustomAttributes.Any(c => c.AttributeType == typeof(WillBeMapAttribute)))
                                         .GetCommonValueOfAttributeToDict();

            var modelAssembly = Assembly.LoadFrom(codeBase + "\\HealthCheck.Model.dll")
                                        .GetTypes()
                                        .Where(x => x.CustomAttributes.Any(c => c.AttributeType == typeof(WillBeMapAttribute)))
                                        .GetCommonValueOfAttributeToDict();

            foreach (var mapping in modelAssembly)
            {

                cfg.CreateMap(domainAssembly[mapping.Key], mapping.Value);
                cfg.CreateMap(mapping.Value, domainAssembly[mapping.Key]);
            }

           

        }


    }
}
