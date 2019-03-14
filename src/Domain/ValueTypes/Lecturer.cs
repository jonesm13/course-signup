namespace Domain.ValueTypes
{
    using Utilities;

    public class Lecturer
    {
        readonly string name;

        public Lecturer(string name)
        {
            Ensure.IsNotNullOrEmpty(name, nameof(name));
            
            this.name = name;
        }

        public static implicit operator Lecturer(string value)
        {
            return new Lecturer(value);
        }

        public override string ToString()
        {
            return name;
        }
    }
}
