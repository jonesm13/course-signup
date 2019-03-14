namespace Domain.Tests
{
    using Aggregates.Course;
    using Exceptions;
    using Xunit;

    public class CourseSignupTests
    {
        [Fact]
        public void WhenPlacesAvailable_CanSignUp()
        {
            // arrange
            Course sut = new Course("Joe Bloggs", 30);

            // act
            sut.SignUp("Arthur Student");
            
            // assert
            // (none required, success is absence of exception)
        }

        [Fact]
        public void WhenNoPlacesAvailable_CannotSignUp()
        {
            Course sut = new Course("Joe Bloggs", 2);

            sut.SignUp("Arthur Student");
            sut.SignUp("Bill Pupil");

            Assert.Throws<DomainException>(() => sut.SignUp("Jenny Delegate"));
        }
    }
}
