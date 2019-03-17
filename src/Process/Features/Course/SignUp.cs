namespace Process.Features.Course
{
    using System;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Aggregates.Course;
    using Domain.Exceptions;
    using Domain.Ports;
    using Domain.ValueTypes;
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

                if(state == default(State))
                {
                    throw new DomainException(
                        "Course not found.",
                        HttpStatusCode.NotFound);
                }

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
