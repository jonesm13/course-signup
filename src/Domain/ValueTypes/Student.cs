namespace Domain.ValueTypes
{
    using Utilities;

    public class Student
    {
        readonly string name;

        public Student(string name)
        {
            Ensure.IsNotNullOrEmpty(name, nameof(name));

            this.name = name;
        }

        public static implicit operator Student(string value)
        {
            return new Student(value);
        }
        
        public override string ToString()
        {
            return name;
        }
    }
}
