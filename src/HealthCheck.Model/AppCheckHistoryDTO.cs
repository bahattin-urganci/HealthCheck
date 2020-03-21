using HealthCheck.Infrastructure.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthCheck.Model
{
    [WillBeMap("AppCheckHistory")]
    public class AppCheckHistoryDTO : BaseDTO
    {
        public int AppId { get; set; }
        public DateTime CheckTime { get; set; }
        public bool Live { get; set; }
        public AppDTO App { get; set; }
    }
}
