namespace Domain.Aggregates.Course
{
    using System.Net;
    using Exceptions;
    using Utilities;
    using ValueTypes;

    public class Course
    {
        State state;

        Course()
        {
            state = new State();
        }

        public Course(Lecturer lecturer, NumberOfPlaces places)
        {
            Ensure.IsNotNull(lecturer, nameof(lecturer));
            Ensure.IsNotNull(places, nameof(places));

            state = new State
            {
                Lecturer = lecturer,
                Places = places,
                AvailablePlaces = places
            };
        }

        public void SignUp(Student student)
        {
            Ensure.IsNotNull(student, nameof(student));

            if(state.AvailablePlaces == 0)
            {
                throw new DomainException(
                    "This course has no available places.",
                    HttpStatusCode.Conflict);
            }

            state.AvailablePlaces--;
        }

        public static Course FromState(State state)
        {
            return new Course { state = state };
        }

        public State ToState()
        {
            return state;
        }
    }
}
