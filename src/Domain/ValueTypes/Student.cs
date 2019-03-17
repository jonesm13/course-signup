namespace Domain.ValueTypes
{
    using Utilities;

    public class Student
    {
        readonly string name;

        public Student(string name, int age)
        {
            Ensure.IsNotNullOrEmpty(name, nameof(name));
            Ensure.IsPositiveInteger(age, nameof(age));

            this.name = name;
        }

        public override string ToString()
        {
            return name;
        }
    }
}
