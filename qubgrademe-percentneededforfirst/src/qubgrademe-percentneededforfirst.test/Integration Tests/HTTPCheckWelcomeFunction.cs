using FluentAssertions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qubgrademe_percentneededforfirst.test
{
    // This is an integration test of checking if the welcome function returns the correct results
    public class HTTPCheckWelcomeFunction
    {
        // Setting up the url that the HTTP server is deployed on
        private string url = "http://qubgrademe-percentneededforfirst.40266405.qpc.hal.davecutting.uk/";

        [Fact]
        public async void testWelcomeContent()
        {
            // Arrange
            using var client = new HttpClient();


            // Act
            var content = await client.GetStringAsync(url);

            // Assert
            content.Should().Be("Welcome to the percent needed for first API");
        }
    }
}
