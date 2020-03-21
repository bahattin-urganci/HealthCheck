using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HealthCheck.Infrastructure.Extensions
{
    public static class TypeExtensions
    {
        public static Dictionary<string, Type> GetCommonValueOfAttributeToDict(this IEnumerable<Type> types)
        {
            Dictionary<string, Type> _dict = new Dictionary<string, Type>();

            foreach (var item in types)
            {
                foreach (var prop in item.CustomAttributes)
                {
                    foreach (var map in prop.ConstructorArguments.First().Value.ToString().Split(','))
                    {
                        _dict.Add(map, item);
                    }


                }
            }

            return _dict;
        }
    }
}
