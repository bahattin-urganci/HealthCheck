using HealthCheck.Infrastructure.Attributes;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HealthCheck.Domain
{
    [WillBeMap("ApplicationUser")]
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

     
    }
}
