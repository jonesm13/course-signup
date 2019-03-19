namespace Process.Features.Course
{
    using System;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using Aspects.Validation;
    using Domain.Aggregates.Course;
    using Domain.Ports;
    using Domain.ValueTypes;
    using FluentValidation;
    using MediatR;
    using Pipeline;

    public class SignUp
    {
        public class Command : IRequest<CommandResult>
        {
            public Guid CourseId { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            readonly IDocumentStore documentStore;

            public Validator(IDocumentStore documentStore)
            {
                this.documentStore = documentStore;

                RuleFor(x => x.CourseId)
                    .MustAsync(Exist)
                    .WithHttpStatusCode(HttpStatusCode.NotFound);

                RuleFor(x => x.Name)
                    .NotEmpty();

                RuleFor(x => x.Age)
                    .Must(BePositiveInteger);
            }

            bool BePositiveInteger(int arg)
            {
                return arg > 0;
            }

            Task<bool> Exist(Guid arg, CancellationToken cancellationToken)
            {
                return documentStore.ExistsAsync(arg.ToString());
            }
        }

        public class Handler : IRequestHandler<Command, CommandResult>
        {
            readonly IDocumentStore documentStore;

            public Handler(IDocumentStore documentStore)
            {
                this.documentStore = documentStore;
            }

            public async Task<CommandResult> Handle(
                Command request,
                CancellationToken cancellationToken)
            {
                State state = await documentStore.GetAsync<State>(
                    request.CourseId.ToString());

                Course course = Course.FromState(state);

                course.SignUp(new Student(request.Name, request.Age));

                await documentStore.StoreAsync(
                    request.CourseId.ToString(),
                    course.ToState());

                return CommandResult.Void
                    .WithNotification(new StudentSignedUp
                    {
                        Age = request.Age,
                        CourseId = request.CourseId,
                        StudentName = request.Name
                    });
            }
        }

        public class StudentSignedUp : INotification
        {
            public Guid CourseId { get; set; }
            public string StudentName { get; set; }
            public int Age { get; set; }
        }
    }
}
