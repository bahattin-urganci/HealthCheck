using System;
using System.IO;
using System.Reflection;

namespace HealthCheck.Infrastructure
{
    public static class Constants
    {
        public static string AppPath => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static string BidigoWeb { get; set; }
    }
}
