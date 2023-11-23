using FlightPlanner.Core.Models;
using FlightPlanner.Models;

namespace FlightPlanner.Core.Services
{
    public interface IFlightService : IEntityService<Flight>
    {
        public Flight? GetFullFlightById(int id);

        bool Exists(Flight flight);

        List<Airport> SearchAirports(string search);

        List<Flight> searchFlights(SearchFlightsRequest searchFlightsRequest);
    }
}
