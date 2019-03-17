namespace Process.Adapters.Nop
{
    using System.Threading.Tasks;
    using Domain.Ports;

    public class NopMessageBus : IMessageBus
    {
        public Task SendAsync(object message)
        {
            return Task.CompletedTask;
        }
    }
}