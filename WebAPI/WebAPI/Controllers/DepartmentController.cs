using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebAPI.Controllers.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _config;

        public DepartmentController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public IActionResult Get()
        {
            // Use the connection string from appsettings.json to connect to the database
            var connectionString = _config.GetConnectionString("DefaultConnection");

            // Create an SqlConnection object
            using var connection = new SqlConnection(connectionString);
            // Create a SqlCommand object
            using var command = new SqlCommand();
            // Assign the connection to the command
            command.Connection = connection;
            command.CommandText = "SELECT * FROM Department";
            // Open the connection
            connection.Open();
            // Execute the command
            var reader = command.ExecuteReader();
            // Read the results into a DataTable
            var table = new DataTable();
            table.Load(reader);
            // Close the reader
            reader.Close();
            // Close the connection
            connection.Close();
            // Return the DataTable as a JSON result
            return new JsonResult(table);
        }

        [HttpPost]
        public IActionResult Post(Department department)
        {
            // Use the connection string from appsettings.json to connect to the database
            var connectionString = _config.GetConnectionString("DefaultConnection");
            // Create an SqlConnection object
            using var connection = new SqlConnection(connectionString);
            // Create a SqlCommand object
            using var command = new SqlCommand();
            // Assign the connection to the command
            command.Connection = connection;
            // Assign the INSERT command to the command object
            command.CommandText = "INSERT INTO dbo.Department (DepartmentName) VALUES (@DepartmentName)";
            // Add the parameters to the command object
            command.Parameters.AddWithValue("@DepartmentName", department.DepartmentName);
            // Open the connection
            connection.Open();
            // Execute the INSERT command
            command.ExecuteNonQuery();
            // Close the connection
            connection.Close();
            // Return JSON about success with 201 Created status code
            return new JsonResult(new { Message = "Department created successfully" }) { StatusCode = 201 };
        }

        [HttpPut]
        public IActionResult Put(Department department)
        {
            // Use the connection string from appsettings.json to connect to the database
            var connectionString = _config.GetConnectionString("DefaultConnection");
            // Create an SqlConnection object
            using var connection = new SqlConnection(connectionString);
            // Create a SqlCommand object
            using var command = new SqlCommand();
            // Assign the connection to the command
            command.Connection = connection;
            // Assign the UPDATE command to the command object
            command.CommandText = "UPDATE dbo.Department SET DepartmentName = @DepartmentName WHERE DepartmentID = @DepartmentID";
            // Add the parameters to the command object
            command.Parameters.AddWithValue("@DepartmentName", department.DepartmentName);
            command.Parameters.AddWithValue("@DepartmentID", department.DepartmentID);
            // Open the connection
            connection.Open();
            // Execute the UPDATE command
            command.ExecuteNonQuery();
            // Close the connection
            connection.Close();
            // Return JSON about success with 200 OK status code
            return new JsonResult(new { Message = "Department updated successfully" }) { StatusCode = 200 };
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Use the connection string from appsettings.json to connect to the database
            var connectionString = _config.GetConnectionString("DefaultConnection");
            // Create an SqlConnection object
            using var connection = new SqlConnection(connectionString);
            // Create a SqlCommand object
            using var command = new SqlCommand();
            // Assign the connection to the command
            command.Connection = connection;
            // Assign the DELETE command to the command object
            command.CommandText = "DELETE FROM dbo.Department WHERE DepartmentID = @DepartmentID";
            // Add the parameters to the command object
            command.Parameters.AddWithValue("@DepartmentID", id);
            // Open the connection
            connection.Open();
            // Execute the DELETE command
            command.ExecuteNonQuery();
            // Close the connection
            connection.Close();
            // Return JSON about success with 200 OK status code
            return new JsonResult(new { Message = "Department deleted successfully" }) { StatusCode = 200 };
        }
    }
}
