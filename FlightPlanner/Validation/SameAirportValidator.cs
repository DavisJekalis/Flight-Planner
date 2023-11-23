using FlightPlanner.Core.Interfaces;
using FlightPlanner.Core.Models;

namespace FlightPlanner.Validation
{
    public class SameAirportValidator : IValidate
    {
        public bool IsValid(Flight flight)
        {
            return flight?.To?.AirportCode?.ToLower()?.Trim() != 
                   flight?.From?.AirportCode?.ToLower()?.Trim();
        }
    }
}
