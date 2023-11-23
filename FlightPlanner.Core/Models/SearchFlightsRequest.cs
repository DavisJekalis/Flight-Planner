namespace FlightPlanner.Models
{
    public class SearchFlightsRequest
    {
        public string From { get; set; }
        public string to { get; set; }
        public string departureDate { get; set; }
    }
}
