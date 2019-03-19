namespace Process.Tests
{
    using System;
    using System.Threading.Tasks;
    using Bases;
    using Features.Course;
    using Xunit;

    public class CanSignUpToCourseFeature : IntegrationTestBase
    {
        [Fact]
        public async Task Test()
        {
            Guid id = Guid.NewGuid();
            
            Create.Command command = new Create.Command
            {
                Id = id,
                Name = "Integration test " + DateTime.UtcNow.Ticks,
                LecturerName = "Joe Bloggs",
                NumberOfPlaces = 10
            };

            await Mediator().Send(command);

            SignUp.Command signUpCommand = new SignUp.Command
            {
                Name = "Tery Student",
                Age = 21,
                CourseId = id
            };
            
            await Mediator().Send(signUpCommand);
        }
    }
}