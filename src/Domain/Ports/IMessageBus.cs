namespace Domain.Ports
{
    using System.Threading.Tasks;

    public interface IMessageBus
    {
        Task SendAsync(object message);
    }
}