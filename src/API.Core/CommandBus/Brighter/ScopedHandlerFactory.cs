using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Paramore.Brighter;

namespace API.Core.CommandBus.Brighter
{
    public class ScopedHandlerFactory : IAmAHandlerFactoryAsync
    {
        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;

        public ScopedHandlerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            var loggerFactory = _serviceProvider.GetRequiredService<ILoggerFactory>();
            _logger = loggerFactory.CreateLogger("HandlerFactory");
        }

        public IHandleRequestsAsync Create(Type handlerType)
        {
            _logger.LogInformation("Invoking handler {HandlerType}", handlerType);

            return (IHandleRequestsAsync)_serviceProvider.GetRequiredService(handlerType);
        }

        public void Release(IHandleRequestsAsync handler)
        {
            // Do Nothing
        }
    }
}
