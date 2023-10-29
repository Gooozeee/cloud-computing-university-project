using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Web.Http;

namespace qubgrademe_percentneededforfirst.Controllers
{
    // Controller which will handle all requests to the API
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class QubGradeMe_PercentNeededForFirstController : ControllerBase
    {
        // Dependency injection for the logger 
        private readonly ILogger<QubGradeMe_PercentNeededForFirstController> _logger;

        // Constructor
        public QubGradeMe_PercentNeededForFirstController(ILogger<QubGradeMe_PercentNeededForFirstController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // Endpoint to greet the user if they call up the API in a browser
        [Microsoft.AspNetCore.Mvc.HttpGet("/", Name = "Welcome")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public ActionResult<string> welcomeUser()
        {
            _logger.LogInformation("The welcome method has been called");
            return Ok("Welcome to the percent needed for first API");
        }

        // Endpoint to calculate the % needed to get a first
        [Microsoft.AspNetCore.Mvc.HttpGet("getPercentNeeded", Name = "getPercent")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public ActionResult<string> getPercentNeededForFirst([FromQuery] Modules modules)
        {
            _logger.LogInformation("The get percent needed for first method has been called");
            int intmark1;
            int intmark2;
            int intmark3;
            int intmark4;
            int intmark5;

            _logger.LogInformation("Validating marks");
            // Checking if the modules are blank
            if (modules.mark1.Trim() == "")
            {
                return BadRequest("{\"Message\": \"You are missing module 1 \"}");
            }
            else if (modules.mark2.Trim() == "")
            {
                return BadRequest("{\"Message\": \"You are missing module 2 \"}");
            }
            else if (modules.mark3.Trim() == "")
            {
                return BadRequest("{\"Message\": \"You are missing module 3 \"}");
            }
            else if (modules.mark4.Trim() == "")
            {
                return BadRequest("{\"Message\": \"You are missing module 4 \"}");
            }
            else if (modules.mark5.Trim() == "")
            {
                return BadRequest("{\"Message\": \"You are missing module 5 \"}");
            }

            _logger.LogInformation("Checking that the marks are integers");
            // Checking if the modules are integers
            if(!Int32.TryParse(modules.mark1,out intmark1))
            {
                return BadRequest("{\"Message\": \"Module 1 needs to be an integer \"}");
            }
            else if (!Int32.TryParse(modules.mark2, out intmark2))
            {
                return BadRequest("{\"Message\": \"Module 2 needs to be an integer \"}");
            }
            else if (!Int32.TryParse(modules.mark3, out intmark3))
            {
                return BadRequest("{\"Message\": \"Module 3 needs to be an integer \"}");
            }
            else if (!Int32.TryParse(modules.mark4, out intmark4))
            {
                return BadRequest("{\"Message\": \"Module 4 needs to be an integer \"}");
            }
            else if (!Int32.TryParse(modules.mark5, out intmark5))
            {
                return BadRequest("{\"Message\": \"Module 5 needs to be an integer \"}");
            }

            _logger.LogInformation("Checking if the mark is between 0 and 100");
            // Checking if the values are >=0 and <=100
            if(intmark1 < 0 || intmark1 > 100)
            {
                return BadRequest("{\"Message\": \"Module 1 needs to be <= 100 or >=0 \"}");
            }
            else if(intmark2 < 0 || intmark2 > 100)
            {
                return BadRequest("{\"Message\": \"Module 2 needs to be <= 100 or >=0 \"}");
            }
            else if (intmark3 < 0 || intmark3 > 100)
            {
                return BadRequest("{\"Message\": \"Module 3 needs to be <= 100 or >=0 \"}");
            }
            else if (intmark4 < 0 || intmark4 > 100)
            {
                return BadRequest("{\"Message\": \"Module 4 needs to be <= 100 or >=0 \"}");
            }
            else if (intmark5 < 0 || intmark5 > 100)
            {
                return BadRequest("{\"Message\": \"Module 5 needs to be <= 100 or >=0 \"}");
            }

            // Calculating the average
            _logger.LogInformation("Calculating the average");
            var sum = intmark1 + intmark2 + intmark3 + intmark4 + intmark5;
            var average = sum / 5;

            // Returning the response
            _logger.LogInformation("Returning the correct message to the user");
            if(average >= 70)
            {
                return Ok("{\"Message\": \"You have already achieved a first with " + average + "%\"}");
            }
            return Ok("{\"Message\": \"You need " + (70 - average) + "% to get a first\"}");
        }
    }
}
