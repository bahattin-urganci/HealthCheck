using HealthCheck.Infrastructure.MailTemplates;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace HealthCheck.Messaging
{
    public class MailService : IMessagingService
    {
        private SmtpClient _smtpClient;
        public MailService(SmtpClient smtpClient)
        {
            _smtpClient = smtpClient;
        }
        public string TemplateName { get; set; }
        public object Props { get; set; }

        public Task Send(List<string> to, string message)
        {
            throw new NotImplementedException();
        }

        public Task Send(List<string> to, string subject, string message)
        {
            throw new NotImplementedException();
        }

        public async Task Send(string to, string subject)
        {
            var body = MailTemplateParser.Parse(TemplateName, Props);
            
            await _smtpClient.SendMailAsync(new MailMessage("test@test.com", to, subject, body));
            
        }

        public async Task Send(string to, string subject, string message)
        {
            throw new NotImplementedException();
        }
    }
}
