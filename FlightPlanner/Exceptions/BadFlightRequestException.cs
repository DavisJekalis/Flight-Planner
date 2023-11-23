namespace FlightPlanner.Exceptions
{
    public class BadFlightRequestException : Exception
    {
        public BadFlightRequestException() : base("Bad Flight Request")
        {
        }
    }
}
