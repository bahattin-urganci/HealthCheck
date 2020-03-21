using CronExpressionDescriptor;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthCheck.Infrastructure.Extensions
{
    public static class CronDescriptor
    {
        public static string EvaluateToCronExpression(this string item)
        {
            Options options = new Options() { ThrowExceptionOnParseError = false };
            options.Use24HourTimeFormat = true;

            item = ExpressionDescriptor.GetDescription(item, options);

            return item;
        }
    }
}
