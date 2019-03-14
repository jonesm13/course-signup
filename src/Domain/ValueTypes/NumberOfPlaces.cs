namespace Domain.ValueTypes
{
    using Utilities;

    public class NumberOfPlaces
    {
        readonly int value;
        
        public NumberOfPlaces(int value)
        {
            Ensure.IsNonNegativeInteger(value, nameof(value));
            
            this.value = value;
        }

        public static implicit operator NumberOfPlaces(int value)
        {
            return new NumberOfPlaces(value);
        }

        public bool None => value == 0;

        public NumberOfPlaces Decrease()
        {
            return new NumberOfPlaces(value - 1);
        }
    }
}
