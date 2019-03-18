namespace Api.IoC
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Domain.Ports;
    using FluentValidation;
    using MediatR;
    using MediatR.Pipeline;
    using Process;
    using Process.Adapters.InMemory;
    using Process.Adapters.Nop;
    using Process.Aspects.Notifications;
    using Process.Pipeline;
    using SimpleInjector;
    using SimpleInjector.Lifestyles;

    public static class ContainerFactory
    {
        public static Container Build()
        {
            Container result = new Container();

            result.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            // mediator
            result.RegisterSingleton<IMediator, Mediator>();
            result.Register(
                () => new ServiceFactory(result.GetInstance),
                Lifestyle.Singleton);

            // pipeline
            result.Collection.Register(typeof(IPipelineBehavior<,>), new []
            {
                typeof(RequestPreProcessorBehavior<,>),
                typeof(RequestPostProcessorBehavior<,>),
                typeof(PipelineBehavior<,>)
            });

            Assembly[] assemblies = GetAssemblies().ToArray();
            
            // pre-processors
            result.Collection.Register(typeof(IRequestPreProcessor<>), assemblies);

            // handlers
            result.Register(typeof(IRequestHandler<,>), assemblies);

            // post-processors
            result.Collection.Register(
                typeof(IRequestPostProcessor<,>),
                new[] { typeof(NotificationsSender<,>) } );

            // validators
            result.Collection.Register(typeof(IValidator<>), assemblies);

            // notification handlers
            result.Collection.Register(
                typeof(INotificationHandler<>),
                assemblies);
            
            // document storage
            result.RegisterSingleton<IDocumentStore, InMemoryDocumentStore>();

            // message bus
            result.Register<IMessageBus, NopMessageBus>();
            
            return result;
        }

        static IEnumerable<Assembly> GetAssemblies()
        {
            yield return typeof(IProcessLivesHere).Assembly;
        }
    }
}