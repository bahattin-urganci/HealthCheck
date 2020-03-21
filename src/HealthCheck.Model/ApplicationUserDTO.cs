using System;
using System.Collections.Generic;
using System.Text;
using HealthCheck.Infrastructure.Attributes;
using Microsoft.AspNetCore.Identity;

namespace HealthCheck.Model
{
    [WillBeMap("ApplicationUser")]
    public class ApplicationUserDTO : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
