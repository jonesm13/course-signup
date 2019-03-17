namespace Domain.Aggregates.Course
{
    using ValueTypes;

    public class State
    {
        public Lecturer Lecturer { get; set; }

        public NumberOfPlaces Places { get; set; }

        public int AvailablePlaces { get; set; }
    }
}