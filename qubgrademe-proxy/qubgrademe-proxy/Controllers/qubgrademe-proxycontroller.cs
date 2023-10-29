using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace qubgrademe_proxy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class qubgrademe_proxycontroller : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<qubgrademe_proxycontroller> _logger;

        // Dependency injection for the configuration (Environment Variables)
        public qubgrademe_proxycontroller(IConfiguration configuration, ILogger<qubgrademe_proxycontroller> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // Endpoint to greet the user if they call up the API in a browser
        [HttpGet("/", Name = "Welcome")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public ActionResult<string> welcomeUser()
        {
            return Ok("Welcome to qubgrademe reverse proxy");
        }

        // Endpoint for the proxy
        [HttpGet("/proxy", Name = "Proxy")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<string>> getResponseFromService([FromQuery] ProxyCommand modules)
        {
            // Getting the endpoint (URL of the correct MicroService)
            var endpoint = _configuration["ConnectionStrings:" + modules.ConnectionString];
            
            _logger.LogInformation("The chosen endpoint is: " + endpoint);

            // If the endpoint doesn't exist in the dictionary then return bad request
            if(endpoint == "" || endpoint == null)
            {
                return BadRequest("This endpoint is invalid");
            }

            // If maxmin or sorted modules is requested the command needs modules, if not then the command only needs marks
            var modulesFlag = false;

            _logger.LogInformation("Checking modules");
            if (modules.Module1 != "" || modules.Module2 != "" || modules.Module3 != "" || modules.Module4 != "" || modules.Module5 != "")
            {
                modulesFlag = true;
            }

            // If there are results for the 
            var saveToDatabaseFlag = false;

            _logger.LogInformation("Checking if save to database is needed");
            if(modules.maxmin != "" || modules.averagegrade != "" || modules.classifygrade != "" || modules.percentneededforfirst != "" || modules.totalmarks != "" || modules.sortedmodules != "")
            {
                saveToDatabaseFlag = true;
            }

            // Making lists of modules and marks so they can be iterated over and validated 
            var moduleList = new List<string>();
            var markList = new List<string>();
            var intMarkList = new List<int>();
            var resultsList = new List<string>();

            _logger.LogInformation("Adding everything to the lists");
            // Adding all of the mocules and marks to the lists
            moduleList.Add(modules.Module1);
            moduleList.Add(modules.Module2);
            moduleList.Add(modules.Module3);
            moduleList.Add(modules.Module4);
            moduleList.Add(modules.Module5);
            markList.Add(modules.Mark1);
            markList.Add(modules.Mark2);
            markList.Add(modules.Mark3);
            markList.Add(modules.Mark4);
            markList.Add(modules.Mark5);
            resultsList.Add(modules.maxmin);
            resultsList.Add(modules.sortedmodules);
            resultsList.Add(modules.classifygrade);
            resultsList.Add(modules.percentneededforfirst);
            resultsList.Add(modules.totalmarks);
            resultsList.Add(modules.averagegrade);

            // Declaring integers for parsing all of the marks
            int intMark1;
            int intMark2;
            int intMark3;
            int intMark4;
            int intMark5;

            // If modules are required for the MicroService check if they are blank
            // If any module is blank, return bad request
  
            if (modulesFlag)
            {
                _logger.LogInformation("Looping through the modules");
                for(int i = 0; i < moduleList.Count(); i++)
                {
                    // Checking if a module is blank
                    if (moduleList[i] == "")
                    {
                        return BadRequest("{\"Message\": \"You are missing module " + i+1 + " \"}");
                    }
                } 
            }

            // Seeing if the marks are integers or not
            if (!Int32.TryParse(modules.Mark1, out intMark1))
            {
                return BadRequest("{\"Message\": \"Mark 1 needs to be an integer \"}");
            }
            else if (!Int32.TryParse(modules.Mark2, out intMark2))
            {
                return BadRequest("{\"Message\": \"Mark 2 needs to be an integer \"}");
            }
            else if (!Int32.TryParse(modules.Mark3, out intMark3))
            {
                return BadRequest("{\"Message\": \"Mark 3 needs to be an integer \"}");
            }
            else if (!Int32.TryParse(modules.Mark4, out intMark4))
            {
                return BadRequest("{\"Message\": \"Mark 4 needs to be an integer \"}");
            }
            else if (!Int32.TryParse(modules.Mark5, out intMark5))
            {
                return BadRequest("{\"Message\": \"Mark 5 needs to be an integer \"}");
            }


            // Checking if the values for the marks are >=0 and <=100
            if (intMark1 < 0 || intMark1 > 100)
            {
                return BadRequest("{\"Message\": \"Mark 1 needs to be <= 100 or >=0 \"}");
            }
            else if (intMark2 < 0 || intMark2 > 100)
            {
                return BadRequest("{\"Message\": \"Mark 2 needs to be <= 100 or >=0 \"}");
            }
            else if (intMark3 < 0 || intMark3 > 100)
            {
                return BadRequest("{\"Message\": \"Mark 3 needs to be <= 100 or >=0 \"}");
            }
            else if (intMark4 < 0 || intMark4 > 100)
            {
                return BadRequest("{\"Message\": \"Mark 4 needs to be <= 100 or >=0 \"}");
            }
            else if (intMark5 < 0 || intMark5 > 100)
            {
                return BadRequest("{\"Message\": \"Mark 5 needs to be <= 100 or >=0 \"}");
            }

            // If the results are required, check if they are blank
            // If any module is blank, return bad request
            if (saveToDatabaseFlag)
            {
                for (int i = 0; i < resultsList.Count(); i++)
                {
                    // Checking if a module is blank
                    if (resultsList[i] == "")
                    {
                        return BadRequest("{\"Message\": \"You are missing a result string\"}");
                    }
                }
            }

            // Adding the integers to a new integer mark list
            intMarkList.Add(intMark1);
            intMarkList.Add(intMark2);
            intMarkList.Add(intMark3);
            intMarkList.Add(intMark4);
            intMarkList.Add(intMark5);

            // Generating the URL for the request
            // If the endpoint needs modules it will also add the modules
            if(modulesFlag)
            {
                for(int i = 0; i < moduleList.Count(); i++)
                {
                    if(i == 0)
                    {
                        endpoint += "?module" + (i + 1) +  "=" + moduleList[i];
                    }
                    else
                    {
                        endpoint += "&module" + (i + 1) + "=" + moduleList[i];
                    }
                }
                for (int i = 0; i < intMarkList.Count(); i++)
                {

                    endpoint += "&mark" + (i + 1) + "=" + intMarkList[i];
                }
            }
            // Else only the marks need added onto the endpoint
            else
            {
                for (int i = 0; i < intMarkList.Count(); i++)
                {
                    if (i == 0)
                    {
                        endpoint += "?mark" + (i + 1) + "=" + intMarkList[i];
                    }
                    else
                    {
                        endpoint += "&mark" + (i + 1) + "=" + intMarkList[i];
                    }
                }
            }
            // Adding on the results to the URL
            if(saveToDatabaseFlag)
            {
                for(int i = 0; i < resultsList.Count(); i++)
                {
                    endpoint += "&result" + (i + 1) + "=" + resultsList[i];
                }   
            }

            // If the username exists in the parameters, add it to the URL to call the save/ retrieve from database API
            if (modules.UserName != "")
            {
                endpoint += "&userName=" + modules.UserName;
            }

            // Calling the microservice using a HTTP Client
            using var client = new HttpClient();
            var response = new HttpResponseMessage();
            try
            {
                response = await client.GetAsync(endpoint);
                _logger.LogInformation(endpoint);
            }
            catch(Exception e)
            {
                _logger.LogInformation("Error with getting response");
                return BadRequest("The call to the neccessary microservice has timed out");
            }

            // Storing the response and code
            string responseBody = await response.Content.ReadAsStringAsync();
            var responseCode = response.StatusCode;

            if(responseCode == HttpStatusCode.OK)
            {
                
                return Ok(responseBody);
            }
            else if(responseCode == HttpStatusCode.BadRequest)
            {
                return BadRequest(responseBody);
            }

            return StatusCode((int)responseCode, "Invalid Request");

        }

        [HttpPost("/addToServiceRegistry", Name = "AddToServiceRegistry")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public ActionResult<string> AddToServiceRegistry([FromBody] URLMapping url)
        {
            _logger.LogInformation("Adding URL for " + url.dnsName);

            _logger.LogInformation("ConnectionStrings:" + url.dnsName);
            var endpoint = _configuration["ConnectionStrings:" + url.dnsName];

            // If the endpoint exists then don't add it again
            if (endpoint != null)
            {
                return BadRequest("This endpoint already exists in the service registry");
            }

            // Create the endpoint in the service registry
            _configuration["ConnectionStrings:" + url.dnsName] = url.url;

            // Returning an OK message
            return Ok("The URL for " + url.dnsName + " has been successfully added to the service registry");
        }

        [HttpPut("/updateServiceRegistry", Name = "UpdateServiceRegistry")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public ActionResult<string> UpdateServiceRegistry([FromBody] URLMapping url)
        {
            _logger.LogInformation("Updating URL for " + url.dnsName);

            var endpoint = _configuration["ConnectionStrings:" + url.dnsName];

            // If the endpoint exists then update that endpoint
            if (endpoint != null)
            {
                // Create the endpoint in the service registry
                _configuration["ConnectionStrings:" + url.dnsName] = url.url;

                // Returning an OK message
                return Ok("The URL for " + url.dnsName + " has been successfully updated in the service registry");
            }

            return BadRequest("This endpoint doesn't yet exist in the service registry so can't be updated");
        }
    }
}
