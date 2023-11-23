﻿using AutoMapper;
using FlightPlanner.Core.Interfaces;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FlightPlanner.Exceptions;
using FlightPlanner.Models;

namespace FlightPlanner.Controllers
{
    [Authorize]
    [Route("admin-api")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly IMapper _mapper;
        private readonly IEnumerable<IValidate> _validators;
        private static readonly object _controllerLock = new();

        public AdminApiController(IFlightService flightService, IMapper mapper, IEnumerable<IValidate> validators)
        {
            _flightService = flightService;
            _mapper = mapper;
            _validators = validators;
        }

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlight(int id)
        {
            lock (_controllerLock)
            {
                var flight = _flightService.GetFullFlightById(id);

                if (flight == null)
                {
                    return NotFound();
                }
                
                return Ok(_mapper.Map<FlightRequest>(flight));
            }
        }

        [Route("flights")]
        [HttpPut]
        public IActionResult PutFlight(FlightRequest request)
        {
            lock (_controllerLock)
            {
                var flight = _mapper.Map<Flight>(request);

                if (!_validators.All(v => v.IsValid(flight)))
                {
                    return BadRequest();
                }

                if (_flightService.Exists(flight))
                {
                    return Conflict();
                }

                _flightService.Create(flight);

                request = _mapper.Map<FlightRequest>(flight);

                return Created("", request);
            }
        }

        [Route("flights/{id}")]
        [HttpDelete]
        public IActionResult DeleteFlight(int id)
        {
            lock (_controllerLock)
            {
                var flight = _flightService.GetFullFlightById(id);

                if (flight == null)
                {
                    return Ok();
                }
                else
                {
                    _flightService.Delete(flight);
                    return Ok();
                }
            }
        }
    }
}
