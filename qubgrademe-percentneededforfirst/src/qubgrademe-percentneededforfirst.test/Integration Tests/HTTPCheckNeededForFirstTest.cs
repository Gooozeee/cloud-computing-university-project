using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using qubgrademe_percentneededforfirst.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qubgrademe_percentneededforfirst.test.Integration_Tests
{
    // This is an integration test to see if the check needed for first function is working correctly online
    public class HTTPCheckNeededForFirstTest
    {
        // Setting up the url that the HTTP server is deployed on
        private string url = "http://qubgrademe-percentneededforfirst.40266405.qpc.hal.davecutting.uk/api/QubGrademe_percentneededforfirst/getpercentneeded";

        // Setting up the logger so that the controller can be instantiated
        private readonly Mock<ILogger<QubGradeMe_PercentNeededForFirstController>> _logger = new();

        [Fact]
        public async void TestPercentNeededForFirstValidDataAtAFirst()
        {
            // Arrange
            var newUrl =  url + "?mark1=70&mark2=70&mark3=70&mark4=70&mark5=70";

            using var client = new HttpClient();

            // Act
            var response = await client.GetAsync(newUrl);

            // Assert
            string responseBody = await response.Content.ReadAsStringAsync();
            responseBody.Should().Be("{\"Message\": \"You have already achieved a first with " + 70 + "%\"}");
        }

        [Fact]
        public async void TestPercentNeededForFirstValidDataAboveAFirst()
        {
            // Arrange 
            var newUrl = url + "?mark1=78&mark2=78&mark3=78&mark4=78&mark5=78";

            using var client = new HttpClient();

            // Act
            var response = await client.GetAsync(newUrl);

            // Assert
            string responseBody = await response.Content.ReadAsStringAsync();
            responseBody.Should().Be("{\"Message\": \"You have already achieved a first with " + 78 + "%\"}");
        }

        [Fact]
        public async void TestPercentNeededForFirstValidDataBelowAFirst()
        {
            // Arrange 
            var newUrl = url + "?mark1=65&mark2=70&mark3=65&mark4=70&mark5=65";

            using var client = new HttpClient();

            // Act
            var response = await client.GetAsync(newUrl);

            // Assert
            string responseBody = await response.Content.ReadAsStringAsync();
            responseBody.Should().Be("{\"Message\": \"You need " + 3 + "% to get a first\"}");
        }

        [Fact]
        public async void TestPercentNeededForFirstMissingmark1()
        {
            // Arrange 
            var newUrl = url + "?mark2=70&mark3=70&mark4=70&mark5=70";

            using var client = new HttpClient();

            // Act
            var response = await client.GetAsync(newUrl);

            // Assert
            string responseBody = await response.Content.ReadAsStringAsync();
            responseBody.Should().Be("{\"Message\": \"You are missing module 1 \"}");
        }

        [Fact]
        public async void TestPercentNeededForFirstMissingmark2()
        {
            // Arrange 
            var newUrl = url + "?mark1=70&mark3=70&mark4=70&mark5=70";

            using var client = new HttpClient();

            // Act
            var response = await client.GetAsync(newUrl);

            // Assert
            string responseBody = await response.Content.ReadAsStringAsync();
            responseBody.Should().Be("{\"Message\": \"You are missing module 2 \"}");
        }

        [Fact]
        public async void TestPercentNeededForFirstMissingmark3()
        {
            // Arrange 
            var newUrl = url + "?mark2=70&mark1=70&mark4=70&mark5=70";

            using var client = new HttpClient();

            // Act
            var response = await client.GetAsync(newUrl);

            // Assert
            string responseBody = await response.Content.ReadAsStringAsync();
            responseBody.Should().Be("{\"Message\": \"You are missing module 3 \"}");
        }

        [Fact]
        public async void TestPercentNeededForFirstMissingmark4()
        {
            // Arrange 
            var newUrl = url + "?mark2=70&mark3=70&mark1=70&mark5=70";

            using var client = new HttpClient();

            // Act
            var response = await client.GetAsync(newUrl);

            // Assert
            string responseBody = await response.Content.ReadAsStringAsync();
            responseBody.Should().Be("{\"Message\": \"You are missing module 4 \"}");
        }

        [Fact]
        public async void TestPercentNeededForFirstMissingmark5()
        {
            // Arrange 
            var newUrl = url + "?mark2=70&mark3=70&mark4=70&mark1=70";

            using var client = new HttpClient();

            // Act
            var response = await client.GetAsync(newUrl);

            // Assert
            string responseBody = await response.Content.ReadAsStringAsync();
            responseBody.Should().Be("{\"Message\": \"You are missing module 5 \"}");
        }

        [Fact]
        public async void TestPercentNeededWithstring()
        {
            // Arrange 
            var newUrl = url + "?mark5=asd&mark2=70&mark3=70&mark4=70&mark1=70";

            using var client = new HttpClient();

            // Act
            var response = await client.GetAsync(newUrl);

            // Assert
            string responseBody = await response.Content.ReadAsStringAsync();
            responseBody.Should().Be("{\"Message\": \"Module 5 needs to be an integer \"}");
        }

        [Fact]
        public async void TestPercentNeededWithmark1LessThan0()
        {
            // Arrange 
            var newUrl = url + "?mark5=70&mark2=70&mark3=70&mark4=70&mark1=-56";

            using var client = new HttpClient();

            // Act
            var response = await client.GetAsync(newUrl);

            // Assert
            string responseBody = await response.Content.ReadAsStringAsync();
            responseBody.Should().Be("{\"Message\": \"Module 1 needs to be <= 100 or >=0 \"}");
        }

        [Fact]
        public async void TestPercentNeededWithmark3GreaterThan100()
        {
            // Arrange 
            var newUrl = url + "?mark5=70&mark2=70&mark3=156&mark4=70&mark1=56";

            using var client = new HttpClient();

            // Act
            var response = await client.GetAsync(newUrl);

            // Assert
            string responseBody = await response.Content.ReadAsStringAsync();
            responseBody.Should().Be("{\"Message\": \"Module 3 needs to be <= 100 or >=0 \"}");
        }
    }
}
