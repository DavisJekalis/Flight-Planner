namespace FlightPlanner.Exceptions
{
    public class InvalidFlightException : Exception
    {
        public InvalidFlightException() : base("Flight start location cannot be the same as ending point")
        {
        }
    }
}
