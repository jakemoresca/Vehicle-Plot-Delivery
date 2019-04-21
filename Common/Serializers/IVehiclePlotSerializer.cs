using Common.DTOs;

namespace Common.Serializers
{
    public interface IVehiclePlotSerializer
    {
        VehiclePlotDto Deserialize(byte[] vehiclePlotBytes);
        byte[] Serialize(VehiclePlotDto vehiclePlot);
    }
}