namespace Process.Features.Course
{
    using System;
    using MediatR;
    using Pipeline;

    public class SignUp
    {
        public class Command : IRequest<CommandResult>
        {
            public Guid CourseId { get; set; }
            public string Name { get; set; }
            public DateTime DateOfBirth { get; set; }
        }
    }
}
