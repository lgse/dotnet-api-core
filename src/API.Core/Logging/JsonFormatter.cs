using System;
using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.Core.Logging
{
    public class JsonFormatter : ConsoleFormatter, IDisposable
    {
        private readonly IDisposable _optionsReloadToken;
        private JsonFormatterOptions _options;

        public JsonFormatter(IOptionsMonitor<JsonFormatterOptions> options) : base("CustomJsonFormatter")
        {
            (_optionsReloadToken, _options) =
                (options.OnChange(ReloadLoggerOptions), options.CurrentValue);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            _optionsReloadToken?.Dispose();
        }

        private void ReloadLoggerOptions(JsonFormatterOptions options)
        {
            _options = options;
        }

        public override void Write<TState>(in LogEntry<TState> logEntry, IExternalScopeProvider scopeProvider, TextWriter textWriter)
        {
            var message = logEntry.Formatter(logEntry.State, logEntry.Exception);

            var contractResolver = new DefaultContractResolver {
                NamingStrategy = new CamelCaseNamingStrategy(),
            };

            var serializerSettings = new JsonSerializerSettings {
                ContractResolver = contractResolver,
                DateFormatString = "yyyy-MM-ddTHH:mm:ss.fffZ",
                Formatting = _options.Indented ? Formatting.Indented : Formatting.None,
                NullValueHandling = NullValueHandling.Ignore,
            };

            var customLogEntry = new {
                At = DateTimeOffset.UtcNow,
                Level = logEntry.LogLevel.ToString(),
                Origin = logEntry.Category,
                Message = message,
            };

            var serializedObject = JsonConvert.SerializeObject(customLogEntry, serializerSettings);
            textWriter.WriteLine(serializedObject);
        }
    }

    public class JsonFormatterOptions : ConsoleFormatterOptions
    {
        public bool Indented { get; set; }
    }
}
