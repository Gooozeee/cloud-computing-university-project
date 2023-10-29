using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Net;

namespace qubgrademe_savetodatabase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class qubgrademesavetodatabasecontroller : ControllerBase
    {
        // Dependency injection for the logger 
        private readonly ILogger<qubgrademesavetodatabasecontroller> _logger;
        private readonly IConfiguration _configuration;

        // Constructor
        public qubgrademesavetodatabasecontroller(IConfiguration configuration, ILogger<qubgrademesavetodatabasecontroller> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // Endpoint to greet the user if they call up the API in a browser
        [Microsoft.AspNetCore.Mvc.HttpGet("/", Name = "Welcome")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public ActionResult<string> welcomeUser()
        {
            return Ok("Welcome to the save to database API");
        }

        // Endpoint for retrieving data from the database for a user
        [Microsoft.AspNetCore.Mvc.HttpGet("/retrieveresults", Name = "RetrieveResults")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public ActionResult<GetUserDetailsRequest> retrieveResultsFromDatabase([FromQuery] GetUserDetailsRequest request)
        { 
            var userFromDb = new UserFromDbRequest();
            
            try
            {
                string connectionString = $"Server=tcp:qubgrademe-database.database.windows.net,1433;Initial Catalog=qubgrademedatabase;Persist Security Info=False;User ID=qubgrademe-database;Password={_configuration["ConnectionStrings:Password"]};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=60;TrustServerCertificate=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    String sql = $"SELECT * FROM [dbo].[Users] WHERE [UserName]= '{request.userName}'";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                userFromDb.userName = reader.GetString(1);
                                userFromDb.module1 = reader.GetString(2);
                                userFromDb.module2 = reader.GetString(3);
                                userFromDb.module3 = reader.GetString(4);
                                userFromDb.module4 = reader.GetString(5);
                                userFromDb.module5 = reader.GetString(6);
                                userFromDb.mark1 = reader.GetString(7);
                                userFromDb.mark2 = reader.GetString(8);
                                userFromDb.mark3 = reader.GetString(9);
                                userFromDb.mark4 = reader.GetString(10);
                                userFromDb.mark5 = reader.GetString(11);
                                userFromDb.results = reader.GetString(12);
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
                return BadRequest(e.ToString());
            }

            if(userFromDb.userName == "")
            {
                return BadRequest("{\"Message\":\"The username " + request.userName +" doesn't have any values in the database\"}");
            }

            return Ok(userFromDb);

        }


        // Endpoint for saving data to the database for a user
        [Microsoft.AspNetCore.Mvc.HttpGet("/addresultstodatabase", Name = "AddResultsToDatabase")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public ActionResult<string> AddResultsToDatabase([FromQuery] UserRequest request)
        {
            // Generating the results string
            var resultsString = "Max Min: " + request.result1 + "\n" + "Sorted Modules: " + request.result2 + "\n" + "Classification: " + request.result3 + "\n" + "Percent needed for first: " + request.result4 + "\n" + request.result5 + "\n" +  request.result6;

            // Checking to see if the user already exists in the database
            var userFromDb = new UserFromDbRequest();
            try
            {
                string connectionString = $"Server=tcp:qubgrademe-database.database.windows.net,1433;Initial Catalog=qubgrademedatabase;Persist Security Info=False;User ID=qubgrademe-database;Password={_configuration["ConnectionStrings:Password"]};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=60;TrustServerCertificate=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    //   INSERT INTO[dbo].[Results] ([UserName], [Module1], [Module2], [Module3], [Module4], [Module5], [Mark1], [Mark2], [Mark3], [Mark4], [Mark5])
                    //   VALUES('Michal', 'CSC-3065', 'CSC-3067', 'CSC-3068', 'CSC-3064', 'CSC-3063', 78, 70, 75, 74, 75);

                    String sql = $"SELECT * FROM [dbo].[Users] WHERE [UserName]= '{request.userName}'";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                userFromDb.userName = reader.GetString(1);
                                userFromDb.module1 = reader.GetString(2);
                                userFromDb.module2 = reader.GetString(3);
                                userFromDb.module3 = reader.GetString(4);
                                userFromDb.module4 = reader.GetString(5);
                                userFromDb.module5 = reader.GetString(6);
                                userFromDb.mark1 = reader.GetString(7);
                                userFromDb.mark2 = reader.GetString(8);
                                userFromDb.mark3 = reader.GetString(9);
                                userFromDb.mark4 = reader.GetString(10);
                                userFromDb.mark5 = reader.GetString(11);
                                userFromDb.results = reader.GetString(12);
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
                return BadRequest(e.ToString());
            }


            // Validating the modules
            var listOfModules = new List<string>();
            listOfModules.Add(request.module1);
            listOfModules.Add(request.module2);
            listOfModules.Add(request.module3);
            listOfModules.Add(request.module4);
            listOfModules.Add(request.module5);

            for(int i = 0; i < listOfModules.Count(); i++)
            {
                if (listOfModules[i].Trim() == "" || listOfModules[i] == null)
                {
                    return BadRequest($"Module {i + 1} is either null or blank and can't be empty");
                }
            }

            // Validating the mark
            int intmark1;
            int intmark2;
            int intmark3;
            int intmark4;
            int intmark5;

            // Checking if the marks are integers
            if (!Int32.TryParse(request.mark1, out intmark1))
            {
                return BadRequest("{\"Message\": \"Module 1 needs to be an integer \"}");
            }
            else if (!Int32.TryParse(request.mark1, out intmark2))
            {
                return BadRequest("{\"Message\": \"Module 2 needs to be an integer \"}");
            }
            else if (!Int32.TryParse(request.mark3, out intmark3))
            {
                return BadRequest("{\"Message\": \"Module 3 needs to be an integer \"}");
            }
            else if (!Int32.TryParse(request.mark4, out intmark4))
            {
                return BadRequest("{\"Message\": \"Module 4 needs to be an integer \"}");
            }
            else if (!Int32.TryParse(request.mark5, out intmark5))
            {
                return BadRequest("{\"Message\": \"Module 5 needs to be an integer \"}");
            }

            // Checking if the values are >=0 and <=100
            if (intmark1 < 0 || intmark1 > 100)
            {
                return BadRequest("{\"Message\": \"Module 1 needs to be <= 100 or >=0 \"}");
            }
            else if (intmark2 < 0 || intmark2 > 100)
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

            // Validating the results
            var resultsList = new List<string>();
            resultsList.Add(request.result1);
            resultsList.Add(request.result2);
            resultsList.Add(request.result3);
            resultsList.Add(request.result4);
            resultsList.Add(request.result5);
            resultsList.Add(request.result6);

            for(int i = 0; i < resultsList.Count; i++)
            {
                if (resultsList[i].Trim() == "" || resultsList[i] == null)
                {
                    return BadRequest($"Result {i + 1} is either null or blank and can't be empty");
                }
            }

            // If the username already exists in the database, update the record that is there
            if (userFromDb.userName != "")
            {
                try
                {
                    string connectionString = $"Server=tcp:qubgrademe-database.database.windows.net,1433;Initial Catalog=qubgrademedatabase;Persist Security Info=False;User ID=qubgrademe-database;Password={_configuration["ConnectionStrings:Password"]};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=60;TrustServerCertificate=True";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        // command.CommandText = "UPDATE Student SET Address = @add, City = @cit Where FirstName = @fn and LastName = @add";

                        String sql = $"UPDATE [dbo].[Users] SET [UserName] = '{request.userName}', [Module1] = '{request.module1}', [Module2] = '{request.module2}', [Module3] = '{request.module3}', [Module4] = '{request.module4}', [Module5] = '{request.module5}', [Mark1] = {request.mark1}, [Mark2] = {request.mark2}, [Mark3] = {request.mark3}, [Mark4] = {request.mark4}, [Mark5] = {request.mark5}, [ResultString] = '{resultsString}';";

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {

                            connection.Open();
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    _logger.LogInformation("{0} {1}", reader.GetString(0), reader.GetString(1));
                                }
                            }
                        }
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.ToString());
                    return BadRequest(e.ToString());
                }

                return Ok("{\"Message\":\"The username " + request.userName + " has been updated in the database\"}");
            }

            try
            {
                string connectionString = $"Server=tcp:qubgrademe-database.database.windows.net,1433;Initial Catalog=qubgrademedatabase;Persist Security Info=False;User ID=qubgrademe-database;Password={_configuration["ConnectionStrings:Password"]};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=60;TrustServerCertificate=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    String sql = $"INSERT INTO[dbo].[Users] ([UserName], [Module1], [Module2], [Module3], [Module4], [Module5], [Mark1], [Mark2], [Mark3], [Mark4], [Mark5], [ResultString]) VALUES('{request.userName}', '{request.module1}', '{request.module2}', '{request.module3}', '{request.module4}', '{request.module5}', {request.mark1}, {request.mark2}, {request.mark3}, {request.mark4}, {request.mark5}, '{resultsString}');";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _logger.LogInformation("{0} {1}", reader.GetString(0), reader.GetString(1));
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
                return BadRequest(e.ToString());
            }

            return Ok("{\"Message\":\"The username " + request.userName +" Saved user to the database\"}");
        }

        // Endpoint for saving monitoring latencies
        [Microsoft.AspNetCore.Mvc.HttpGet("/addLatencies", Name = "AddLatencies")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public ActionResult<string> AddLatencies([FromQuery] AddLatencyRequest request)
        {
            try
            {
                string connectionString = $"Server=tcp:qubgrademe-database.database.windows.net,1433;Initial Catalog=qubgrademedatabase;Persist Security Info=False;User ID=qubgrademe-database;Password={_configuration["ConnectionStrings:Password"]};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=60;TrustServerCertificate=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    String sql = $"INSERT INTO[dbo].[Latencies] ([Proxy], [ProxyBackup], [MaxMin], [SortedModules], [ClassifyGrade], [PercentNeededForFirst], [TotalMarks], [AverageGrade], [Retrieve], [SaveToDb], [DateSaved], [TimeSaved]) VALUES({request.Proxy}, {request.ProxyBackup}, {request.MaxMin}, {request.SortedModules}, {request.ClassifyGrade}, {request.PercentNeededForFirst}, {request.TotalMarks}, {request.AverageGrade}, {request.Retrieve}, {request.SaveToDb}, '{DateTime.Now.ToString("MM-dd-yyyy")}', '{DateTime.Now.ToString("HH:mm:ss")}');";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _logger.LogInformation("{0} {1}", reader.GetString(0), reader.GetString(1));
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
                return BadRequest(e.ToString());
            }

            return Ok("{\"Message\":\"The latencies have been saved into the database\"}");
        }

        // Endpoint for retrieving latencies from the database
        // ONLY LATENCIES FROM THE SAME DAY ARE RETRIEVED
        [Microsoft.AspNetCore.Mvc.HttpGet("/retrievelatencies", Name = "RetrieveLatencies")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public ActionResult<List<AddLatencyRequest>> retrieveLatencies()
        {
            
            var listOfLatencies = new List<AddLatencyRequest>();

            try
            {
                string connectionString = $"Server=tcp:qubgrademe-database.database.windows.net,1433;Initial Catalog=qubgrademedatabase;Persist Security Info=False;User ID=qubgrademe-database;Password={_configuration["ConnectionStrings:Password"]};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=60;TrustServerCertificate=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    String sql = $"SELECT * FROM [dbo].[Latencies] WHERE [DateSaved] = '{DateTime.Now.ToString("MM-dd-yyyy")}';";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var latenciesFromDb = new AddLatencyRequest();
                                latenciesFromDb.Proxy = reader.GetInt32(1);
                                latenciesFromDb.ProxyBackup = reader.GetInt32(2);
                                latenciesFromDb.MaxMin = reader.GetInt32(3);
                                latenciesFromDb.SortedModules = reader.GetInt32(4);
                                latenciesFromDb.ClassifyGrade = reader.GetInt32(5);
                                latenciesFromDb.PercentNeededForFirst = reader.GetInt32(6);
                                latenciesFromDb.TotalMarks = reader.GetInt32(7);
                                latenciesFromDb.AverageGrade = reader.GetInt32(8);
                                latenciesFromDb.Retrieve = reader.GetInt32(9);
                                latenciesFromDb.SaveToDb = reader.GetInt32(10);
                                latenciesFromDb.dateSaved = reader.GetString(12);
                                listOfLatencies.Add(latenciesFromDb);
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
                return BadRequest(e.ToString());
            }

            return Ok(listOfLatencies);

        }
    }
}
