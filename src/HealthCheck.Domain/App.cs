using HealthCheck.Infrastructure.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HealthCheck.Domain
{
    [WillBeMap("App")]
    public class App : BaseEntity
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public string Interval { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public virtual ICollection<AppCheckHistory> AppCheckHistories { get; set; }
    }
}
