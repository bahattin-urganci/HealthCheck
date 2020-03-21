using System;

namespace HealthCheck.Logger
{
    public class Log
    {
        public string RequestId { get; set; }
        public DateTime RequestTime { get; set; }
        public DateTime ResponseTime { get; set; }
        public double Elapsed { get; set; }
        public string Route { get; set; }

        public string Request { get; set; }
        public string Response { get; set; }

    }
}
