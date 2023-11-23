namespace FlightPlanner.Exceptions
{
    public class FlightConflictException : Exception
    {
        public FlightConflictException() : base("Conflict in current flight")
        {
        }
    }
}
