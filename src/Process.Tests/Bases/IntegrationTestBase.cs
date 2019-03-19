namespace Process.Tests.Bases
{
    using System;
    using Doubles;
    using IoC;
    using MediatR;

    public abstract class IntegrationTestBase
    {
        protected Func<IMediator> Mediator { get; }
        
        protected Func<InMemoryNotificationStore> Notifications { get; }
        
        public IntegrationTestBase()
        {
            Mediator = () => ContainerFactory.Instance.GetInstance<IMediator>();
            Notifications = () => ContainerFactory.Instance.GetInstance<InMemoryNotificationStore>();
        }
    }
}
