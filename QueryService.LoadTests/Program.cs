using System;
using System.Net.Http;
using System.Threading.Tasks;
using NBomber.Contracts;
using NBomber.CSharp;

namespace QueryService.LoadTests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Query Service Load Tests");

            const string QueryServiceUrl = @"http://localhost:5000/api/vehiclejourney/1/2019-4-24T10:24:00Z";
            var queryServiceClient = new HttpClient();

            // first, you need to create a step
            var step1 = Step.Create("Query vehicle plots", ConnectionPool.None, async context =>
            {
                var output = await queryServiceClient.GetAsync(QueryServiceUrl);
                return Response.Ok();
            });

            // after creating a step you should add it to Scenario.
            var scenario = ScenarioBuilder.CreateScenario("Get average number of queries can handle per second", step1)
                            .WithConcurrentCopies(1)
                            .WithWarmUpDuration(TimeSpan.FromSeconds(1));

            // run scenario via NBomberRunner
            NBomberRunner.RegisterScenarios(scenario)
                         .RunInConsole();
        }
    }
}
