namespace Process.Features.Course
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Aggregates.Course;
    using Domain.Ports;
    using MediatR;
    using Pipeline;

    public class Create
    {
        public class Command : IRequest<CommandResult>
        {
            public Guid Id { get; set; }
            public string Lecturer { get; set; }
            public int NumberOfPlaces { get; set; }
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
                    request.Lecturer,
                    request.NumberOfPlaces);
                
                await documentStore.StoreAsync(
                    request.Id.ToString(),
                    course.ToState());

                return CommandResult.Void;
            }
        }
    }
}