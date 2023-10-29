using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using qubgrademe_percentneededforfirst.Controllers;
using System.Net;

namespace qubgrademe_percentneededforfirst.test
{
    // This is a unit test of the welcome function
    public class WelcomeTest
    { 
        // Setting up the logger so that the controller can be instantiated
        private readonly Mock<ILogger<QubGradeMe_PercentNeededForFirstController>> _logger = new();

        [Fact]
        public void TestWelcomeString()
        {
            // Arrange 
            var controller = new QubGradeMe_PercentNeededForFirstController(_logger.Object);

            // Act
            var result = controller.welcomeUser();

            // Assert
            OkObjectResult? okResult = result.Result as OkObjectResult;

            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<string>>();
            okResult.Should().BeOfType<OkObjectResult>();
            okResult.Value.Should().Be("Welcome to the percent needed for first API");
        }
    }
}