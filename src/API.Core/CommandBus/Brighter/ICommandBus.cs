using System.Threading.Tasks;

namespace API.Core.CommandBus.Brighter
{
    public interface ICommandBus
    {
        public Task SendAsync<T>(T command) where T : Command;
    }
}
