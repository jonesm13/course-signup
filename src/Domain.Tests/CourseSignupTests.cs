namespace Domain.Tests
{
    using Xunit;

    public class CourseSignupTests
    {
        [Fact]
        public void WhenPlacesAvailable_CanSignUp()
        {
            Assert.True(1==2);
        }

        [Fact]
        public void WhenCourseIsFull_CannotSignUp()
        {
            Assert.True(1==2);
        }
    }
}
