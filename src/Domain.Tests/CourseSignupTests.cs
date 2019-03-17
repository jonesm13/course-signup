namespace Domain.Tests
{
    using Aggregates.Course;
    using Exceptions;
    using ValueTypes;
    using Xunit;

    public class CourseSignupTests
    {
        [Fact]
        public void WhenPlacesAvailable_CanSignUp()
        {
            // arrange
            Course sut = new Course("Joe Bloggs", 30);

            // act
            sut.SignUp(new Student("Arthur Student", 18));
            
            // assert
            // (none required, success is absence of exception)
        }

        [Fact]
        public void WhenNoPlacesAvailable_CannotSignUp()
        {
            Course sut = new Course("Joe Bloggs", 2);

            sut.SignUp(new Student("Arthur Student", 18));
            sut.SignUp(new Student("Bill Pupil", 19));

            Assert.Throws<DomainException>(
                () => sut.SignUp(new Student("Jenny Delegate", 30)));
        }
    }
}
