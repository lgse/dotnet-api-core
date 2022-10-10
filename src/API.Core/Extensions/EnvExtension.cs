using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;

namespace API.Core.Extensions
{
    public static class EnvExtensions
    {
        public const string Ci = "Ci";
        public const string Testing = "Testing";

        public static bool IsCi(this IHostEnvironment hostEnvironment)
        {
            if (hostEnvironment == null) {
                throw new ArgumentNullException(nameof(hostEnvironment));
            }

            return hostEnvironment.IsEnvironment(Ci);
        }

        public static bool IsTesting(this IHostEnvironment hostEnvironment)
        {
            if (hostEnvironment == null) {
                throw new ArgumentNullException(nameof(hostEnvironment));
            }

            return hostEnvironment.IsEnvironment(Testing);
        }

        public static IHostEnvironment TestingEnvironment()
        {
            return new HostingEnvironment {
                EnvironmentName = "Testing",
            };
        }
    }
}
