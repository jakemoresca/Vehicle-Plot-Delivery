using Microsoft.AspNetCore.Mvc;
using QueryServiceBackEnd.Services;
using QueryServiceWeb.Mappers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace QueryService.Controllers
{
    [ApiController]
    public class VehicleJourneyController : ControllerBase
    {
        private readonly IVehicleJourneyService _vehicleJourneyService;
        private readonly IVehicleJourneyMapper _vehicleJourneyMapper;

        public VehicleJourneyController(IVehicleJourneyService vehicleJourneyService, IVehicleJourneyMapper vehicleJourneyMapper)
        {
            _vehicleJourneyService = vehicleJourneyService;
            _vehicleJourneyMapper = vehicleJourneyMapper;
        }

        //api/vehiclejourney/1/2019-4-24T10:24:00Z/2019-4-24T10:35:00Z
        [HttpGet("api/{controller}/{id}/{timeStart}/{timeEnd?}")]
        public async Task<IActionResult> GetAsync(int id, DateTime timeStart, DateTime? timeEnd)
        {
            var vehicleJourneys = await _vehicleJourneyService.FindVehicleJourneyAsync(id, timeStart.ToUniversalTime(), timeEnd ?? DateTime.UtcNow);
            var vehicleJourneyDtos = vehicleJourneys.Select(_vehicleJourneyMapper.ToDto);
            return Ok(vehicleJourneyDtos);
        }
    }
}