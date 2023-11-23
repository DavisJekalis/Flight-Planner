using Microsoft.AspNetCore.Mvc;
using FlightPlanner.Core.Services;
using AutoMapper;
using FlightPlanner.Core.Interfaces;
using FlightPlanner.Models;

namespace FlightPlanner.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerFlightApiController : ControllerBase
    {
        protected readonly IFlightService _flightService;
        private readonly IMapper _mapper;

        public CustomerFlightApiController(IFlightService flightService, IMapper mapper)
        {
            _flightService = flightService;
            _mapper = mapper;
        } 

        [Route("airports")]
        [HttpGet]
        public IActionResult GetAirport([FromQuery] string search)
        {
            var airports = _flightService.SearchAirports(search.Trim().ToLower());

            if (airports == null)
            {
                BadRequest();
            }

            var airportRequests = _mapper.Map<List<AirportRequest>>(airports);

            return Ok(airportRequests);
        }

        [Route("flights/search")]
        [HttpPost]
        public IActionResult SearchFlight(SearchFlightsRequest search)
        {
            if (search.From == search.to)
            {
                return BadRequest();
            }

            var flights = _flightService.searchFlights(search);

            if (flights == null)
            {
                return Ok(new PageResult() { Items = new FlightRequest[0], Page = 0, TotalItems = 0 });
            }

            var mappedFlights = _mapper.Map<List<FlightRequest>>(flights);

            return Ok(new PageResult() { Items = mappedFlights.ToArray(), Page = 0, TotalItems = flights.Count });
        }

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult FindFlightById(int id)
        {
            var flight = _flightService.GetFullFlightById(id);

            if (flight == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<FlightRequest>(flight);

            return Ok(result);
        }
    }
}
