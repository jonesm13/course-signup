namespace Domain.Tests
{
    using System;
    using ValueTypes;
    using Xunit;

    public class StudentTests
    {
        [Fact]
        public void CannotCreateStudentWithNegativeAge()
        {
            Assert.Throws<ArgumentException>(() => new Student("Jonny Student", -1));
        }
        
        [Fact]
        public void CannotCreateStudentWithNullName()
        {
            Assert.Throws<ArgumentNullException>(() => new Student(null, 20));
        }
    }
}