namespace Process.Tests
{
    using System;
    using System.Threading.Tasks;
    using Bases;
    using Features.Course;
    using IoC;
    using SimpleInjector.Lifestyles;
    using Xunit;

    public class CanSignUpToCourseFeature : IntegrationTestBase
    {
        [Fact]
        public async Task Test()
        {
            using(AsyncScopedLifestyle.BeginScope(ContainerFactory.Instance))
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
                    Name = "Terry Student",
                    Age = 21,
                    CourseId = id
                };
            
                await Mediator().Send(signUpCommand);

                Assert.True(Notifications().WasReceived<Create.CourseCreated>());
                Assert.True(Notifications().WasReceived<SignUp.StudentSignedUp>());
            }
        }
    }
}