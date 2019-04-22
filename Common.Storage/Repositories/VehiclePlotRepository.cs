using Common.Models;
using Common.Storage.Daos;
using Common.Storage.Factories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Storage.Repositories
{
    public class VehiclePlotRepository : IVehiclePlotRepository
    {
        private readonly IVehiclePlotFactory _vehiclePlotFactory;
        private readonly IVehiclePlotDao _vehiclePlotDao;

        public VehiclePlotRepository(IVehiclePlotFactory vehiclePlotFactory, IVehiclePlotDao vehiclePlotDao)
        {
            _vehiclePlotFactory = vehiclePlotFactory;
            _vehiclePlotDao = vehiclePlotDao;
        }

        public async Task<bool> InsertAsync(VehiclePlot vehiclePlot)
        {
            var vehiclePlotDto = _vehiclePlotFactory.ToDto(vehiclePlot);
            return await _vehiclePlotDao.InsertAsync(vehiclePlotDto);
        }

        public async Task<List<VehiclePlot>> FindAllVehiclePlotsAsync(string id, double score)
        {
            var vehiclePlotValues = await _vehiclePlotDao.FindAllVehiclePlotsAsync(id, score);
            return vehiclePlotValues.Select(_vehiclePlotFactory.ToModel).ToList();
        }
    }
}
