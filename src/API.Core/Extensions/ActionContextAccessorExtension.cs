using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace API.Core.Extensions
{
    public static class ActionContextExtension
    {
        public static Guid TryGetId(this IActionContextAccessor context, string key)
        {
            if (context.ActionContext == null) return Guid.NewGuid();

            var candidate = context.ActionContext.RouteData.Values
                .Where(x => x.Key == key)
                .Select(x => x.Value)
                .FirstOrDefault();

            if (candidate is null) {
                throw new Exception($"Could not parse route parameter `{key}`");
            }

            return new Guid(candidate.ToString() ?? string.Empty);

        }
    }
}
