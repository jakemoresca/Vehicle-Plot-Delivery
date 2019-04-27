using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Models;

namespace QueryServiceBackEnd.Services
{
    public interface IVehicleJourneyService
    {
        Task<List<VehicleJourney>> FindVehicleJourneyAsync(int vehicleId, DateTime timeStart, DateTime timeEnd);
    }
}