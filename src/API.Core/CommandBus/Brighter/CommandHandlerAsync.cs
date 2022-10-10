using System.Threading;
using System.Threading.Tasks;
using Paramore.Brighter;

namespace API.Core.CommandBus.Brighter
{
    public abstract class CommandHandlerAsync<T> : RequestHandlerAsync<T> where T : class, IRequest
    {
        public override async Task<T> HandleAsync(T command, CancellationToken cancellationToken = default)
        {
            await HandleAsync(command);

            return await base.HandleAsync(command, CancellationToken.None);
        }

        protected abstract Task HandleAsync(T command);
    }
}
