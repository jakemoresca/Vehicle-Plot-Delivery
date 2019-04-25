using Common.Storage.Daos;
using Moq;
using StackExchange.Redis;
using System.Threading.Tasks;
using Xunit;

namespace Common.Storage.UnitTests.Daos
{
    public class VehiclePlotDaoTests
    {
        private readonly Mock<IConnectionMultiplexer> _connectionMultiplexer;
        private readonly Mock<IDatabase> _database;

        public VehiclePlotDaoTests()
        {
            _database = new Mock<IDatabase>();

            _connectionMultiplexer = new Mock<IConnectionMultiplexer>();
            _connectionMultiplexer.Setup(x => x.GetDatabase(It.IsAny<int>(), It.IsAny<object>()))
                .Returns(_database.Object);
        }

        [Theory]
        [InlineData(1, "definition", 124.1)]
        [InlineData(2, "", -151)]
        [InlineData(3, "definition with space", 0)]
        public async Task Should_insert_vehicle_plot(int id, string definition, double score)
        {
            //Arrange
            _database.Setup(x => x.SortedSetAddAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<double>(), It.IsAny<When>(), It.IsAny<CommandFlags>()));

            var vehiclePlotDto = new VehiclePlotDto
            {
                Id = id,
                Definition = definition,
                Score = score
            };

            var Sut = new VehiclePlotDao(_connectionMultiplexer.Object);

            //Act
            await Sut.InsertAsync(vehiclePlotDto);

            //Assert
            _database.Verify(x => x.SortedSetAddAsync($"vehicle-plot-delivery:vehicle:{vehiclePlotDto.Id}", vehiclePlotDto.Definition, vehiclePlotDto.Score,
                When.Always, CommandFlags.None));
        }

        [Theory]
        [InlineData(1, 123.45)]
        [InlineData(2, 0)]
        [InlineData(3, 12414141)]
        public async Task Should_find_all_vehicle_plots(int id, double score)
        {
            //Arrange
            _database.Setup(x => x.SortedSetRangeByScoreAsync(It.IsAny<string>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<Exclude>(),
                It.IsAny<Order>(), It.IsAny<long>(), It.IsAny<long>(), It.IsAny<CommandFlags>()))
                .ReturnsAsync(new RedisValue[] { });

            var Sut = new VehiclePlotDao(_connectionMultiplexer.Object);

            //Act
            await Sut.FindAllVehiclePlotsAsync(id, score);

            //Assert
            _database.Verify(x => x.SortedSetRangeByScoreAsync($"vehicle-plot-delivery:vehicle:{id}", score, double.PositiveInfinity, 
                Exclude.None, Order.Ascending, 0, -1, CommandFlags.None));
        }
    }
}
