using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using qubgrademe_percentneededforfirst.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qubgrademe_percentneededforfirst.test
{
    // This is a unit test of the percent needed for first function
    public class PercentNeededForFirstTest
    {
        // Setting up the logger so that the controller can be instantiated
        private readonly Mock<ILogger<QubGradeMe_PercentNeededForFirstController>> _logger = new();

        [Fact]
        public void TestPercentNeededForFirstValidDataAtAFirst()
        {
            // Arrange 
            var controller = new QubGradeMe_PercentNeededForFirstController(_logger.Object);
            Modules modules = new Modules
            {
                mark1 = "70",
                mark2 = "70",
                mark3 = "70",
                mark4 = "70",
                mark5 = "70"
            };

            // Act
            var result = controller.getPercentNeededForFirst(modules);

            // Assert
            OkObjectResult? okResult = result.Result as OkObjectResult;

            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<string>>();
            okResult.Should().BeOfType<OkObjectResult>();
            okResult.Should().NotBeNull();
            okResult.Value.Should().Be("{\"Message\": \"You have already achieved a first with " + 70 + "%\"}");
        }

        [Fact]
        public void TestPercentNeededForFirstValidDataAboveAFirst()
        {
            // Arrange 
            var controller = new QubGradeMe_PercentNeededForFirstController(_logger.Object);
            Modules modules = new Modules
            {
                mark1 = "75",
                mark2 = "70",
                mark3 = "75",
                mark4 = "70",
                mark5 = "75"
            };

            // Act
            var result = controller.getPercentNeededForFirst(modules);

            // Assert
            OkObjectResult? okResult = result.Result as OkObjectResult;

            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<string>>();
            okResult.Should().BeOfType<OkObjectResult>();
            okResult.Should().NotBeNull();
            okResult.Value.Should().Be("{\"Message\": \"You have already achieved a first with " + 73 + "%\"}");
        }

        [Fact]
        public void TestPercentNeededForFirstValidDataBelowAFirst()
        {
            // Arrange 
            var controller = new QubGradeMe_PercentNeededForFirstController(_logger.Object);
            Modules modules = new Modules
            {
                mark1 = "70",
                mark2 = "65",
                mark3 = "70",
                mark4 = "65",
                mark5 = "65"
            };

            // Act
            var result = controller.getPercentNeededForFirst(modules);

            // Assert
            OkObjectResult? okResult = result.Result as OkObjectResult;

            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<string>>();
            okResult.Should().BeOfType<OkObjectResult>();
            okResult.Should().NotBeNull();
            okResult.Value.Should().Be("{\"Message\": \"You need " + 3 + "% to get a first\"}");
        }

        [Fact]
        public void TestPercentNeededForFirstMissingmark1()
        {
            // Arrange 
            var controller = new QubGradeMe_PercentNeededForFirstController(_logger.Object);
            Modules modules = new Modules
            {
                mark5 = "70",
                mark2 = "70",
                mark3 = "70",
                mark4 = "70",
            };

            // Act
            var result = controller.getPercentNeededForFirst(modules);

            // Assert
            BadRequestObjectResult? okResult = result.Result as BadRequestObjectResult;

            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<string>>();
            okResult.Should().BeOfType<BadRequestObjectResult>();
            okResult.Should().NotBeNull();
            okResult.Value.Should().Be("{\"Message\": \"You are missing module 1 \"}");
        }

        [Fact]
        public void TestPercentNeededForFirstMissingmark2()
        {
            // Arrange 
            var controller = new QubGradeMe_PercentNeededForFirstController(_logger.Object);
            Modules modules = new Modules
            {
                mark1 = "70",
                mark5 = "70",
                mark3 = "70",
                mark4 = "70",
            };

            // Act
            var result = controller.getPercentNeededForFirst(modules);

            // Assert
            BadRequestObjectResult? okResult = result.Result as BadRequestObjectResult;

            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<string>>();
            okResult.Should().BeOfType<BadRequestObjectResult>();
            okResult.Should().NotBeNull();
            okResult.Value.Should().Be("{\"Message\": \"You are missing module 2 \"}");
        }

        [Fact]
        public void TestPercentNeededForFirstMissingmark3()
        {
            // Arrange 
            var controller = new QubGradeMe_PercentNeededForFirstController(_logger.Object);
            Modules modules = new Modules
            {
                mark1 = "70",
                mark2 = "70",
                mark5 = "70",
                mark4 = "70",
            };

            // Act
            var result = controller.getPercentNeededForFirst(modules);

            // Assert
            BadRequestObjectResult? okResult = result.Result as BadRequestObjectResult;

            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<string>>();
            okResult.Should().BeOfType<BadRequestObjectResult>();
            okResult.Should().NotBeNull();
            okResult.Value.Should().Be("{\"Message\": \"You are missing module 3 \"}");
        }

        [Fact]
        public void TestPercentNeededForFirstMissingmark4()
        {
            // Arrange 
            var controller = new QubGradeMe_PercentNeededForFirstController(_logger.Object);
            Modules modules = new Modules
            {
                mark1 = "70",
                mark2 = "70",
                mark3 = "70",
                mark5 = "70",
            };

            // Act
            var result = controller.getPercentNeededForFirst(modules);

            // Assert
            BadRequestObjectResult? okResult = result.Result as BadRequestObjectResult;

            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<string>>();
            okResult.Should().BeOfType<BadRequestObjectResult>();
            okResult.Should().NotBeNull();
            okResult.Value.Should().Be("{\"Message\": \"You are missing module 4 \"}");
        }

        [Fact]
        public void TestPercentNeededForFirstMissingmark5()
        {
            // Arrange 
            var controller = new QubGradeMe_PercentNeededForFirstController(_logger.Object);
            Modules modules = new Modules
            {
                mark1 = "70",
                mark2 = "70",
                mark3 = "70",
                mark4 = "70",
            };

            // Act
            var result = controller.getPercentNeededForFirst(modules);

            // Assert
            BadRequestObjectResult? okResult = result.Result as BadRequestObjectResult;

            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<string>>();
            okResult.Should().NotBeNull();
            okResult.Should().BeOfType<BadRequestObjectResult>();
            okResult.Value.Should().Be("{\"Message\": \"You are missing module 5 \"}");
        }

        [Fact]
        public void TestPercentNeededForFirstmark5String()
        {
            // Arrange 
            var controller = new QubGradeMe_PercentNeededForFirstController(_logger.Object);
            Modules modules = new Modules
            {
                mark1 = "70",
                mark2 = "70",
                mark3 = "70",
                mark4 = "70",
                mark5 = "asd"
            };

            // Act
            var result = controller.getPercentNeededForFirst(modules);

            // Assert
            BadRequestObjectResult? okResult = result.Result as BadRequestObjectResult;

            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<string>>();
            okResult.Should().NotBeNull();
            okResult.Should().BeOfType<BadRequestObjectResult>();
            okResult.Value.Should().Be("{\"Message\": \"Module 5 needs to be an integer \"}");
        }

        [Fact]
        public void TestPercentNeededForFirstmark5Below0()
        {
            // Arrange 
            var controller = new QubGradeMe_PercentNeededForFirstController(_logger.Object);
            Modules modules = new Modules
            {
                mark1 = "70",
                mark2 = "70",
                mark3 = "70",
                mark4 = "70",
                mark5 = "-54"
            };

            // Act
            var result = controller.getPercentNeededForFirst(modules);

            // Assert
            BadRequestObjectResult? okResult = result.Result as BadRequestObjectResult;

            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<string>>();
            okResult.Should().NotBeNull();
            okResult.Should().BeOfType<BadRequestObjectResult>();
            okResult.Value.Should().Be("{\"Message\": \"Module 5 needs to be <= 100 or >=0 \"}");
        }

        [Fact]
        public void TestPercentNeededForFirstmark4Above100()
        {
            // Arrange 
            var controller = new QubGradeMe_PercentNeededForFirstController(_logger.Object);
            Modules modules = new Modules
            {
                mark1 = "70",
                mark2 = "70",
                mark3 = "70",
                mark4 = "187",
                mark5 = "70"
            };

            // Act
            var result = controller.getPercentNeededForFirst(modules);

            // Assert
            BadRequestObjectResult? okResult = result.Result as BadRequestObjectResult;

            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<string>>();
            okResult.Should().NotBeNull();
            okResult.Should().BeOfType<BadRequestObjectResult>();
            okResult.Value.Should().Be("{\"Message\": \"Module 4 needs to be <= 100 or >=0 \"}");
        }
    }
}
