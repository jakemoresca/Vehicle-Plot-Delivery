using Common.Storage.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace QueryService.Controllers
{
    [ApiController]
    public class VehiclePlotController : ControllerBase
    {
        private readonly IVehiclePlotService _vehiclePlotService;

        public VehiclePlotController(IVehiclePlotService vehiclePlotService)
        {
            _vehiclePlotService = vehiclePlotService;
        }

        //api/vehicleplot/1/2019-4-24T10:24:00Z
        [HttpGet("api/{controller}/{id}/{timestamp?}")]
        public async Task<IActionResult> GetAsync(int id, DateTime? timestamp)
        {
            var vehiclePlots = await _vehiclePlotService.FindAllVehiclePlotsAsync(id, timestamp ?? DateTime.UtcNow);
            return Ok(vehiclePlots);
        }
    }
}