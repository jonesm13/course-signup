namespace Process.Features.Course
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Ports;
    using MediatR;
    using Pipeline;

    public class RequestSignUp
    {
        public class Command : IRequest<CommandResult>
        {
            public Guid CourseId { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
        }

        public class Handler : IRequestHandler<Command, CommandResult>
        {
            readonly IMessageBus bus;

            public Handler(IMessageBus bus)
            {
                this.bus = bus;
            }

            public async Task<CommandResult> Handle(
                Command request,
                CancellationToken cancellationToken)
            {
                await bus.SendAsync(request);
                return CommandResult.Void;
            }
        }
    }
}