using HealthCheck.Infrastructure.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HealthCheck.Model
{
    [WillBeMap("App")]
    public class AppDTO : BaseDTO
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public string Interval { get; set; }
        public ApplicationUserDTO User { get; set; }
        public List<AppCheckHistoryDTO> AppCheckHistories { get; set; }
    }
}
