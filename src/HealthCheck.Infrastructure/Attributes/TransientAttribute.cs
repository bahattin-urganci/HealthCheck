using System;
using System.Collections.Generic;
using System.Text;

namespace HealthCheck.Infrastructure.Attributes
{
    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class TransientAttribute : Attribute
    {

    }
}
