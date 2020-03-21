using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthCheck.ExceptionHandler
{
    public class ErrorMessage
    {
        public int StatusCode { get; set; }
        public string Header { get; set; }
        public string Message { get; set; }
        public string RequestId { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
