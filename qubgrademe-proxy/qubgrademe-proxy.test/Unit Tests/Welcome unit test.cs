
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using qubgrademe_proxy.Controllers;

namespace qubgrademe_proxy.test
{
    // This is a unit test of the welcome method
    public class WelcomeUnitTest
    {
        // Setting up the logger so that the controller can be instantiated
        private readonly Mock<ILogger<qubgrademe_proxycontroller>> _logger = new();

        [Fact]
        public void TestWelcomeString()
        {
            // Arrange 
            var inMemorySettings = new Dictionary<string, string> {
                {"ConnectionStrings:MaxMin", "http://qubgrademe-maxmin.40266405.qpc.hal.davecutting.uk/" },
                {"ConnectionStrings:SortedModules", "http://qubgrademe-sortmodules.40266405.qpc.hal.davecutting.uk/"},
                {"ConnectionStrings:ClassifyGrade", "https://qubgrademe-classifygrade.azurewebsites.net/api/qubgrademe-classifygrade"},
                {"ConnectionStrings:PercentNeededForFirst", "http://qubgrademe-percentneededforfirst.40266405.qpc.hal.davecutting.uk/api/QubGrademe_percentneededforfirst/getpercentneeded"},
                {"ConnectionStrings:TotalMarks", "http://qubgrademe-totalmarks.40266405.qpc.hal.davecutting.uk/totalmarks"},
                {"ConnectionStrings:AverageGrade", "http://qubgrademe-averagegrade.40266405.qpc.hal.davecutting.uk/averagegrade"},

                {"Modules:MaxMin", "true"},
                {"Modules:SortedModules", "true"},
                {"Modules:ClassifyGrade", "false"},
                {"Modules:PercentNeededForFirst", "false"},
                {"Modules:TotalMarks", "false"},
                {"Modules:AverageGrade", "false"},
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var controller = new qubgrademe_proxycontroller(configuration, _logger.Object);

            // Act
            var result = controller.welcomeUser();

            // Assert
            OkObjectResult? okResult = result.Result as OkObjectResult;

            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<string>>();
            okResult.Should().BeOfType<OkObjectResult>();
            okResult.Value.Should().Be("Welcome to qubgrademe reverse proxy");
        }
    }
}