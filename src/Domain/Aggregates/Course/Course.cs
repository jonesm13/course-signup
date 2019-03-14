namespace Domain.Aggregates.Course
{
    using System.Net;
    using Exceptions;
    using Utilities;
    using ValueTypes;

    public class Course
    {
        readonly Lecturer lecturer;
        readonly NumberOfPlaces places;

        NumberOfPlaces availablePlaces;

        public Course(Lecturer lecturer, NumberOfPlaces places)
        {
            Ensure.IsNotNull(lecturer, nameof(lecturer));
            Ensure.IsNotNull(places, nameof(places));
            
            this.lecturer = lecturer;
            this.places = places;

            availablePlaces = places;
        }

        public void SignUp(Student student)
        {
            Ensure.IsNotNull(student, nameof(student));

            if(availablePlaces.None)
            {
                throw new DomainException(
                    "This course has no available places.",
                    HttpStatusCode.Conflict);
            }

            availablePlaces = availablePlaces.Decrease();
        }
    }
}
