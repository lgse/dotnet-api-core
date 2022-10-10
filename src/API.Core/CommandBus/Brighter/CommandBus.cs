using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Paramore.Brighter;

namespace API.Core.CommandBus.Brighter
{
    public class CommandBus : ICommandBus
    {
        private readonly IAmACommandProcessor _commandBus;

        private readonly ILogger _logger;

        public CommandBus(IAmACommandProcessor commandBus, ILoggerFactory loggerFactory)
        {
            _commandBus = commandBus;
            _logger = loggerFactory.CreateLogger("CommandBus");
        }

        public Task SendAsync<T>(T command) where T : Command
        {
            _logger.LogInformation("{Command} command was dispatched", typeof(T));
            return _commandBus.SendAsync(command);
        }
    }
}
