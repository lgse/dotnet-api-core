using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.Core.Extensions
{
    public static class StringExtension
    {
        public static MemoryStream GenerateMemoryStreamFromString(this string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static string ToJsonString<T>(this T value)
        {
            var contractResolver = new DefaultContractResolver {
                NamingStrategy = new CamelCaseNamingStrategy(),
            };

            var serializerSettings = new JsonSerializerSettings {
                ContractResolver = contractResolver,
                DateFormatString = "yyyy-MM-ddTHH:mm:ss.fffZ",
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore,
            };

            return JsonConvert.SerializeObject(value, serializerSettings);
        }

        public static string AppendRandomString(this string source)
        {
            return string.Concat(source, Guid.NewGuid().ToString("n").AsSpan(0, 8));
        }
    }
}
