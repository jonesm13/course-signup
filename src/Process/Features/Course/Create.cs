namespace Process.Features.Course
{
    using System;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using Aspects.Validation;
    using Domain.Aggregates.Course;
    using Domain.Ports;
    using FluentValidation;
    using MediatR;
    using Pipeline;

    public class Create
    {
        public class Command : IRequest<CommandResult>
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string LecturerName { get; set; }
            public int NumberOfPlaces { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            readonly IDocumentStore store;

            public Validator(IDocumentStore store)
            {
                this.store = store;

                RuleFor(x => x.Id)
                    .MustAsync(NotAlreadyExist)
                    .WithHttpStatusCode(HttpStatusCode.Conflict);

                RuleFor(x => x.Name)
                    .NotEmpty();

                RuleFor(x => x.LecturerName)
                    .NotEmpty();

                RuleFor(x => x.NumberOfPlaces)
                    .Must(BeAPositiveInteger);
            }

            async Task<bool> NotAlreadyExist(
                Guid arg,
                CancellationToken cancellationToken)
            {
                bool result = await store.ExistsAsync(arg.ToString());
                return !result;
            }

            bool BeAPositiveInteger(int arg)
            {
                return arg > 0;
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
                Course course = new Course(
                    request.LecturerName,
                    request.NumberOfPlaces);
                
                await documentStore.StoreAsync(
                    request.Id.ToString(),
                    course.ToState());

                return CommandResult.Void
                    .WithNotification(new CourseCreated
                    {
                        Name = request.Name,
                        CourseId = request.Id,
                        Places = request.NumberOfPlaces
                    });
            }
        }

        public class CourseCreated : INotification
        {
            public Guid CourseId { get; set; }
            public string Name { get; set; }
            public int Places { get; set; }
        }
    }
}