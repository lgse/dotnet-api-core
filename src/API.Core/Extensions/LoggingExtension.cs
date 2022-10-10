using System;
using Microsoft.Extensions.Logging;
using API.Core.Logging;

namespace API.Core.Extensions
{
    public static class LoggingExtension
    {
        public static ILoggingBuilder AddCustomJsonConsole(this ILoggingBuilder builder)
        {
            return builder.AddConsole(options => options.FormatterName = "CustomJsonFormatter").AddConsoleFormatter<JsonFormatter, JsonFormatterOptions>();
        }

        public static ILoggingBuilder AddCustomJsonConsole(this ILoggingBuilder builder, Action<JsonFormatterOptions> configure)
        {
            return builder.AddConsole(options => options.FormatterName = "CustomJsonFormatter").AddConsoleFormatter<JsonFormatter, JsonFormatterOptions>(configure);
        }
    }
}
