using CommandLine;

namespace SenderClient
{
    internal class CommandLineOptions
    {
        [Option("vid", Required = true, HelpText = "Vehicle Id to use for sending vehicle plots")]
        public int VehicleId { get; set; }

        [Option("interval", Default = 1000, HelpText = "Sending interval in milliseconds")]
        public int Interval { get; set; }

    }
}
