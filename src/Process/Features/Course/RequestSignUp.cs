namespace Process.Features.Course
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Ports;
    using FluentValidation;
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

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Name)
                    .NotEmpty();

                RuleFor(x => x.Age)
                    .Must(BeAPositiveInteger);
            }

            bool BeAPositiveInteger(int arg)
            {
                return arg > 0;
            }
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