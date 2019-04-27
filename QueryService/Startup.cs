using Common.Storage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using QueryServiceBackEnd;
using QueryServiceWeb.Mappers;

namespace QueryService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IVehicleJourneyMapper, VehicleJourneyMapper>();
            QueryServiceBackEndDependencyInjection.Register(services);
            CommonStorageDepedencyInjection.Register(services);

            services.AddMvc()
                .AddJsonOptions(x =>
                {
                    x.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                    x.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }
}
