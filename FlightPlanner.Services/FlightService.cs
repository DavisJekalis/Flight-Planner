using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using FlightPlanner.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Services
{
    public class FlightService : EntityService<Flight>, IFlightService
    {
        public FlightService(IFlightPlannerDbContext context) : base(context)
        {
        }

        public Flight? GetFullFlightById(int id)
        {
            return _context.Flights
                .Include(f => f.From)
                .Include(f => f.To)
                .SingleOrDefault(f => f.Id == id);
        }

        public bool Exists(Flight flight)
        {
            return _context.Flights.Any(f => f.ArrivalTime == flight.ArrivalTime &&
                                             f.DepartureTime == flight.DepartureTime &&
                                             f.Carrier == flight.Carrier &&
                                             f.To.AirportCode == flight.To.AirportCode &&
                                             f.From.AirportCode == flight.From.AirportCode);
        }

        public List<Airport> SearchAirports(string search)
        {
            search = search.ToUpper();

            var fromAirports = _context.Flights.Select(flight => flight.From);
            var toAirports = _context.Flights.Select(flight => flight.To);

            var airports = fromAirports.Concat(toAirports)
                .Where(a => a.AirportCode.ToUpper().Contains(search) ||
                            a.City.ToUpper().Contains(search) ||
                            a.Country.ToUpper().Contains(search)).ToList();

            if (airports != null)
            {
                return airports;
            }

            return null;
        }

        public List<Flight> searchFlights(SearchFlightsRequest searchFlightsRequest)
        {
            if (string.IsNullOrEmpty(searchFlightsRequest.From) || string.IsNullOrEmpty(searchFlightsRequest.to) ||
                string.IsNullOrEmpty(searchFlightsRequest.departureDate))
            {
                return null;
            }

            if (searchFlightsRequest.From.Trim().ToUpper() == searchFlightsRequest.to.ToUpper())
            {
                return null;
            }

            var departureAirport = SearchAirports(searchFlightsRequest.From).FirstOrDefault();
            var arrivalAirport = SearchAirports(searchFlightsRequest.to).FirstOrDefault();

            if (departureAirport == null || arrivalAirport == null)
            {
                return null;
            }

            var flights = _context.Flights
                .Where(flight => flight.From.AirportCode == departureAirport.AirportCode &&
                                 flight.To.AirportCode == arrivalAirport.AirportCode)
                .ToList();

            flights = flights
                .Where(flight => flight.DepartureTime.Contains(searchFlightsRequest.departureDate))
                .ToList();

            return flights;
        }
    }
}