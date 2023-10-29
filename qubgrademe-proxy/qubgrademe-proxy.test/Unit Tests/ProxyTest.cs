using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using qubgrademe_proxy.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qubgrademe_proxy.test.Unit_Tests
{
    // Unit/integration (Haven't mocked out the API call) tests for the proxy
    public class ProxyTest
    {
        // Setting up the logger so that the controller can be instantiated
        private readonly Mock<ILogger<qubgrademe_proxycontroller>> _logger = new();

        // TestMaxMin
        [Fact]
        public async void TestMaxMin()
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

            var proxyCommand = new ProxyCommand
            {
                ConnectionString = "MaxMin",
                Module1 = "CSC-3065",
                Module2 = "CSC-3061",
                Module3 = "CSC-3067",
                Module4 = "CSC-3068",
                Module5 = "CSC-3062",
                Mark1 = "78",
                Mark2 = "75",
                Mark3 = "79",
                Mark4 = "80",
                Mark5 = "75"
            };

            // Act
            var result = await controller.getResponseFromService(proxyCommand);

            // Assert
            OkObjectResult? okResult = result.Result as OkObjectResult;

            string shouldBe = "{\"error\":false,\"modules\":[\"CSC-3065\",\"CSC-3061\",\"CSC-3067\",\"CSC-3068\",\"CSC-3062\"],\"marks\":[\"78\",\"75\",\"79\",\"80\",\"75\"],\"max_module\":\"CSC-3068 - 80\",\"min_module\":\"CSC-3062 - 75\"}";

            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<string>>();
            okResult.Should().BeOfType<OkObjectResult>();
            okResult.Value.Should().Be(shouldBe);

        }

        // Test sorted modules
        [Fact]
        public async void TestSortedModules()
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

            var proxyCommand = new ProxyCommand
            {
                ConnectionString = "SortedModules",
                Module1 = "CSC-3065",
                Module2 = "CSC-3061",
                Module3 = "CSC-3067",
                Module4 = "CSC-3068",
                Module5 = "CSC-3062",
                Mark1 = "78",
                Mark2 = "75",
                Mark3 = "79",
                Mark4 = "80",
                Mark5 = "75"
            };

            // Act
            var result = await controller.getResponseFromService(proxyCommand);

            // Assert
            OkObjectResult? okResult = result.Result as OkObjectResult;

            string shouldBe = "{\"error\":false,\"modules\":[\"CSC-3065\",\"CSC-3061\",\"CSC-3067\",\"CSC-3068\",\"CSC-3062\"],\"marks\":[\"78\",\"75\",\"79\",\"80\",\"75\"],\"sorted_modules\":[{\"module\":\"CSC-3068\",\"marks\":\"80\"},{\"module\":\"CSC-3067\",\"marks\":\"79\"},{\"module\":\"CSC-3065\",\"marks\":\"78\"},{\"module\":\"CSC-3061\",\"marks\":\"75\"},{\"module\":\"CSC-3062\",\"marks\":\"75\"}]}";

            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<string>>();
            okResult.Should().BeOfType<OkObjectResult>();
            okResult.Value.Should().Be(shouldBe);
        }

        // Test classify grade
        [Fact]
        public async void TestClassifyGrade()
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

            var proxyCommand = new ProxyCommand
            {
                ConnectionString = "ClassifyGrade",
                Module1 = "CSC-3065",
                Module2 = "CSC-3061",
                Module3 = "CSC-3067",
                Module4 = "CSC-3068",
                Module5 = "CSC-3062",
                Mark1 = "78",
                Mark2 = "75",
                Mark3 = "79",
                Mark4 = "80",
                Mark5 = "75"
            };

            // Act
            var result = await controller.getResponseFromService(proxyCommand);

            // Assert
            OkObjectResult? okResult = result.Result as OkObjectResult;

            string shouldBe = "{\"Classification\" :\"First Class\"}";

            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<string>>();
            okResult.Should().BeOfType<OkObjectResult>();
            okResult.Value.Should().Be(shouldBe);
        }

        // Test percent needed for first
        [Fact]
        public async void TestSortedPercentNeededForFirst()
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

            var proxyCommand = new ProxyCommand
            {
                ConnectionString = "PercentNeededForFirst",
                Module1 = "CSC-3065",
                Module2 = "CSC-3061",
                Module3 = "CSC-3067",
                Module4 = "CSC-3068",
                Module5 = "CSC-3062",
                Mark1 = "78",
                Mark2 = "75",
                Mark3 = "79",
                Mark4 = "80",
                Mark5 = "75"
            };

            // Act
            var result = await controller.getResponseFromService(proxyCommand);

            // Assert
            OkObjectResult? okResult = result.Result as OkObjectResult;

            string shouldBe = "{\"Message\": \"You have already achieved a first with 77%\"}";

            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<string>>();
            okResult.Should().BeOfType<OkObjectResult>();
            okResult.Value.Should().Be(shouldBe);
        }

        // Test total marks
        [Fact]
        public async void TestTotalMarks()
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

            var proxyCommand = new ProxyCommand
            {
                ConnectionString = "TotalMarks",
                Module1 = "CSC-3065",
                Module2 = "CSC-3061",
                Module3 = "CSC-3067",
                Module4 = "CSC-3068",
                Module5 = "CSC-3062",
                Mark1 = "78",
                Mark2 = "75",
                Mark3 = "79",
                Mark4 = "80",
                Mark5 = "75"
            };

            // Act
            var result = await controller.getResponseFromService(proxyCommand);

            // Assert
            OkObjectResult? okResult = result.Result as OkObjectResult;

            string shouldBe = "{\"message\":\"Your total marks are: 387";

            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<string>>();
            okResult.Should().BeOfType<OkObjectResult>();
            okResult.Value.ToString().Contains(shouldBe).Should().BeTrue();
        }

        // Test average grade
        [Fact]
        public async void TestAverageGrade()
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

            var proxyCommand = new ProxyCommand
            {
                ConnectionString = "AverageGrade",
                Module1 = "CSC-3065",
                Module2 = "CSC-3061",
                Module3 = "CSC-3067",
                Module4 = "CSC-3068",
                Module5 = "CSC-3062",
                Mark1 = "78",
                Mark2 = "75",
                Mark3 = "79",
                Mark4 = "80",
                Mark5 = "75"
            };

            // Act
            var result = await controller.getResponseFromService(proxyCommand);

            // Assert
            OkObjectResult? okResult = result.Result as OkObjectResult;

            string shouldBe = "{\"Message\": \"Your average grade is: 77\"}";

            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<string>>();
            okResult.Should().BeOfType<OkObjectResult>();
            okResult.Value.Should().Be(shouldBe);
        }

        // Test wrong connection string
        [Fact]
        public async void TestInvalidConnectionString()
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

            var proxyCommand = new ProxyCommand
            {
                ConnectionString = "AverageGradeasdf",
                Module1 = "CSC-3065",
                Module2 = "CSC-3061",
                Module3 = "CSC-3067",
                Module4 = "CSC-3068",
                Module5 = "CSC-3062",
                Mark1 = "78",
                Mark2 = "75",
                Mark3 = "79",
                Mark4 = "80",
                Mark5 = "75"
            };

            // Act
            var result = await controller.getResponseFromService(proxyCommand);

            // Assert
            BadRequestObjectResult? okResult = result.Result as BadRequestObjectResult;

            string shouldBe = "This endpoint is invalid";

            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<string>>();
            okResult.Should().BeOfType<BadRequestObjectResult>();
            okResult.Value.Should().Be(shouldBe);
        }

        // Test missing module
        [Fact]
        public async void TestMissingModule()
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

            var proxyCommand = new ProxyCommand
            {
                ConnectionString = "MaxMin",
                //Module1 = "CSC-3065",
                Module2 = "CSC-3061",
                Module3 = "CSC-3067",
                Module4 = "CSC-3068",
                Module5 = "CSC-3062",
                Mark1 = "78",
                Mark2 = "75",
                Mark3 = "79",
                Mark4 = "80",
                Mark5 = "75"
            };

            // Act
            var result = await controller.getResponseFromService(proxyCommand);

            // Assert
            BadRequestObjectResult? okResult = result.Result as BadRequestObjectResult;

            string shouldBe = "{\"Message\": \"You are missing module 01 \"}";

            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<string>>();
            okResult.Should().BeOfType<BadRequestObjectResult>();
            okResult.Value.Should().Be(shouldBe);
        }

        // Test missing mark
        [Fact]
        public async void TestMissingMark()
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

            var proxyCommand = new ProxyCommand
            {
                ConnectionString = "AverageGrade",
                Module1 = "CSC-3065",
                Module2 = "CSC-3061",
                Module3 = "CSC-3067",
                Module4 = "CSC-3068",
                Module5 = "CSC-3062",
                //Mark1 = "78",
                Mark2 = "75",
                Mark3 = "79",
                Mark4 = "80",
                Mark5 = "75"
            };

            // Act
            var result = await controller.getResponseFromService(proxyCommand);

            // Assert
            BadRequestObjectResult? okResult = result.Result as BadRequestObjectResult;

            string shouldBe = "{\"Message\": \"Mark 1 needs to be an integer \"}";

            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<string>>();
            okResult.Should().BeOfType<BadRequestObjectResult>();
            okResult.Value.Should().Be(shouldBe);
        }

        // Test mark above 100
        [Fact]
        public async void TestMarkAbove100()
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

            var proxyCommand = new ProxyCommand
            {
                ConnectionString = "AverageGrade",
                Module1 = "CSC-3065",
                Module2 = "CSC-3061",
                Module3 = "CSC-3067",
                Module4 = "CSC-3068",
                Module5 = "CSC-3062",
                Mark1 = "156",
                Mark2 = "75",
                Mark3 = "79",
                Mark4 = "80",
                Mark5 = "75"
            };

            // Act
            var result = await controller.getResponseFromService(proxyCommand);

            // Assert
            BadRequestObjectResult? okResult = result.Result as BadRequestObjectResult;

            string shouldBe = "{\"Message\": \"Mark 1 needs to be <= 100 or >=0 \"}";

            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<string>>();
            okResult.Should().BeOfType<BadRequestObjectResult>();
            okResult.Value.Should().Be(shouldBe);
        }

        // Test mark above 100
        [Fact]
        public async void TestMarkBelow0()
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

            var proxyCommand = new ProxyCommand
            {
                ConnectionString = "AverageGrade",
                Module1 = "CSC-3065",
                Module2 = "CSC-3061",
                Module3 = "CSC-3067",
                Module4 = "CSC-3068",
                Module5 = "CSC-3062",
                Mark1 = "-25",
                Mark2 = "75",
                Mark3 = "79",
                Mark4 = "80",
                Mark5 = "75"
            };

            // Act
            var result = await controller.getResponseFromService(proxyCommand);

            // Assert
            BadRequestObjectResult? okResult = result.Result as BadRequestObjectResult;

            string shouldBe = "{\"Message\": \"Mark 1 needs to be <= 100 or >=0 \"}";

            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<string>>();
            okResult.Should().BeOfType<BadRequestObjectResult>();
            okResult.Value.Should().Be(shouldBe);
        }

        // Test mark as string
        [Fact]
        public async void TestMarkAsString()
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

            var proxyCommand = new ProxyCommand
            {
                ConnectionString = "AverageGrade",
                Module1 = "CSC-3065",
                Module2 = "CSC-3061",
                Module3 = "CSC-3067",
                Module4 = "CSC-3068",
                Module5 = "CSC-3062",
                Mark1 = "adsf",
                Mark2 = "75",
                Mark3 = "79",
                Mark4 = "80",
                Mark5 = "75"
            };

            // Act
            var result = await controller.getResponseFromService(proxyCommand);

            // Assert
            BadRequestObjectResult? okResult = result.Result as BadRequestObjectResult;

            string shouldBe = "{\"Message\": \"Mark 1 needs to be an integer \"}";

            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<string>>();
            okResult.Should().BeOfType<BadRequestObjectResult>();
            okResult.Value.Should().Be(shouldBe);
        }
    }
}
