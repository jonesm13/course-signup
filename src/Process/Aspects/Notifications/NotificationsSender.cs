namespace Process.Aspects.Notifications
{
    using System.Threading.Tasks;
    using MediatR.Pipeline;

    public class NotificationsSender<TRequest, TResponse>
        : IRequestPostProcessor<TRequest, TResponse>
    {
        public Task Process(TRequest request, TResponse response)
        {
            throw new System.NotImplementedException();
        }
    }
}