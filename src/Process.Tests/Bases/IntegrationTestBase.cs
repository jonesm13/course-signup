namespace Process.Tests.Bases
{
    using System;
    using System.Reflection;
    using Adapters.InMemory;
    using Adapters.Nop;
    using Aspects.Notifications;
    using Domain.Ports;
    using Doubles;
    using FluentValidation;
    using MediatR;
    using MediatR.Pipeline;
    using Microsoft.Extensions.Logging;
    using Pipeline;
    using SimpleInjector;
    using SimpleInjector.Lifestyles;

    public abstract class IntegrationTestBase
    {
        static Container container;

        protected Func<IMediator> Mediator =>
            () => container.GetInstance<IMediator>();
        
        public IntegrationTestBase()
        {
            container = new Container();
            container.Options.DefaultScopedLifestyle =
                new AsyncScopedLifestyle();

            // mediator
            container.RegisterSingleton<IMediator, Mediator>();
            container.Register(
                () => new ServiceFactory(container.GetInstance),
                Lifestyle.Singleton);

            // pipeline
            container.Collection.Register(typeof(IPipelineBehavior<,>), new []
            {
                typeof(RequestPreProcessorBehavior<,>),
                typeof(RequestPostProcessorBehavior<,>),
                typeof(PipelineBehavior<,>)
            });

            Assembly[] assemblies = { typeof(IProcessLivesHere).Assembly };
            
            // pre-processors
            container.Collection.Register(typeof(IRequestPreProcessor<>), assemblies);

            // handlers
            container.Register(typeof(IRequestHandler<,>), assemblies);

            // post-processors
            container.Collection.Register(
                typeof(IRequestPostProcessor<,>),
                new[] { typeof(NotificationsSender<,>) } );

            // validators
            container.Collection.Register(typeof(IValidator<>), assemblies);

            // notification handlers
            container.Collection.Register(
                typeof(INotificationHandler<>),
                assemblies);
            
            // document storage
            container.RegisterSingleton<IDocumentStore, InMemoryDocumentStore>();

            // message bus
            container.Register<IMessageBus, NopMessageBus>();

            // logging
            container.Register<ILogger, NopLogger>();

            container.Verify();
        }
    }
}
