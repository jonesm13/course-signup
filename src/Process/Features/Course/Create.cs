namespace Process.Features.Course
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
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
            public Validator()
            {
                RuleFor(x => x.Name)
                    .NotEmpty();

                RuleFor(x => x.LecturerName)
                    .NotEmpty();

                RuleFor(x => x.NumberOfPlaces)
                    .Must(BeAPositiveInteger);
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