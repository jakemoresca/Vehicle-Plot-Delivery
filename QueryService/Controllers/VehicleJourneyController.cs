using Microsoft.AspNetCore.Mvc;
using QueryServiceBackEnd.Services;
using System;
using System.Threading.Tasks;

namespace QueryService.Controllers
{
    [ApiController]
    public class VehicleJourneyController : ControllerBase
    {
        private readonly IVehicleJourneyService _vehicleJourneyService;

        public VehicleJourneyController(IVehicleJourneyService vehicleJourneyService)
        {
            _vehicleJourneyService = vehicleJourneyService;
        }

        //api/vehicleplot/1/2019-4-24T10:24:00Z/2019-4-24T10:35:00Z
        [HttpGet("api/{controller}/{id}/{timeStart}/{timeEnd?}")]
        public async Task<IActionResult> GetAsync(int id, DateTime timeStart, DateTime? timeEnd)
        {
            var vehicleJourneys = await _vehicleJourneyService.FindVehicleJourneyAsync(id, timeStart, timeEnd ?? DateTime.UtcNow);
            return Ok(vehicleJourneys);
        }
    }
}