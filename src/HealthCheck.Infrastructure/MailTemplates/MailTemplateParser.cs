using HandlebarsDotNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HealthCheck.Infrastructure.MailTemplates
{
    public static class MailTemplateParser
    {
        public static string Parse(string templateName, object parameters)
        {
            var source = File.ReadAllText(Path.Combine(Constants.AppPath, "MailTemplates", templateName));
            var template = Handlebars.Compile(source);
            var result = template(parameters);
            return result;
        }
    }
}
