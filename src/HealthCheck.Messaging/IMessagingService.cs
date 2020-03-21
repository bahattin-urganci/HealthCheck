using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthCheck.Messaging
{
    public interface IMessagingService
    {
        string TemplateName { get; set; }
        object Props { get; set; }
        Task Send(List<string> to, string subject);
        Task Send(List<string> to, string subject, string message);
        Task Send(string to, string subject);
        Task Send(string to, string subject, string message);
    }
}
