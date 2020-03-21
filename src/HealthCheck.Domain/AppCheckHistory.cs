using HealthCheck.Infrastructure.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HealthCheck.Domain
{
    [WillBeMap("AppCheckHistory")]
    public class AppCheckHistory :BaseEntity
    {
        public int AppId { get; set; }
        public DateTime CheckTime { get; set; }
        public bool Live { get; set; }
        [ForeignKey("AppId")]
        public virtual App App { get; set; }
    }
}
