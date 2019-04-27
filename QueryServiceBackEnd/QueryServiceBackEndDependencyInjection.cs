using Microsoft.Extensions.DependencyInjection;
using QueryServiceBackEnd.Factories;
using QueryServiceBackEnd.Services;

namespace QueryServiceBackEnd
{
    public static class QueryServiceBackEndDependencyInjection
    {
        public static void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IVehicleJourneyFactory, VehicleJourneyFactory>();
            serviceCollection.AddScoped<IVehicleJourneyService, VehicleJourneyService>();
        }
    }
}
