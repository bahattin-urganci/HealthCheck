using System;
using System.Collections.Generic;
using System.Text;

namespace HealthCheck.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ScopedAttribute : Attribute
    {
    }
}
