using System.Data;
using System.Data.SqlClient;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebAPI.Controllers.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _config;

        public EmployeeController(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// List all employees
        /// </summary>
        /// <returns>API result</returns>
        [HttpGet]
        public IActionResult Get()
        {
            var connectionString = _config.GetConnectionString("DefaultConnection");
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand();
            
            command.Connection = connection;
            command.CommandText = "SELECT EmployeeId, EmployeeName, Department, " +
                                  "convert(varchar(10),DateOfJoining,120) as DateOfJoining, " +
                                  "PhotoFileName FROM dbo.Employee";
            connection.Open();
            var reader = command.ExecuteReader();
            var table = new DataTable();
            table.Load(reader);
            reader.Close();
            connection.Close();

            return new JsonResult(table) { StatusCode = StatusCodes.Status200OK };
        }

        /// <summary>
        /// Add new employee
        /// </summary>
        /// <param name="employee">Employee to create</param>
        /// <returns>API result</returns>
        [HttpPost]
        public IActionResult Post(Employee employee)
        {
            var connectionString = _config.GetConnectionString("DefaultConnection");
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand();
            
            command.Connection = connection;
            command.CommandText = "INSERT INTO dbo.Employee (EmployeeName, Department, DateOfJoining, PhotoFileName) " +
                                  "VALUES (@Name, @Department, @DateOfJoining, @PhotoFileName)";
            command.Parameters.AddWithValue("@Name", employee.EmployeeName);
            command.Parameters.AddWithValue("@Department", employee.Department);
            command.Parameters.AddWithValue("@DateOfJoining", employee.DateOfJoining);
            command.Parameters.AddWithValue("@PhotoFileName", employee.PhotoFileName);
            
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            
            return new JsonResult(new { Message = "Employee created successfully" })
                { StatusCode = StatusCodes.Status201Created };
        }

        /// <summary>
        /// Update employee
        /// </summary>
        /// <param name="employee">Employee to update</param>
        /// <returns>API result</returns>
        [HttpPut]
        public IActionResult Put(Employee employee)
        {
            var connectionString = _config.GetConnectionString("DefaultConnection");
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand();
            
            command.Connection = connection;
            command.CommandText = "UPDATE dbo.Employee SET EmployeeName = @Name, Department = @Department, " +
                                  "DateOfJoining = @DateOfJoining, PhotoFileName = @PhotoFileName " +
                                  "WHERE EmployeeId = @Id";
            command.Parameters.AddWithValue("@Id", employee.EmployeeId);
            command.Parameters.AddWithValue("@Name", employee.EmployeeName);
            command.Parameters.AddWithValue("@Department", employee.Department);
            command.Parameters.AddWithValue("@DateOfJoining", employee.DateOfJoining);
            command.Parameters.AddWithValue("@PhotoFileName", employee.PhotoFileName);
            
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            
            return new JsonResult(new { Message = "Employee updated successfully" })
            { StatusCode = StatusCodes.Status200OK };
        }

        /// <summary>
        /// Delete employee
        /// </summary>
        /// <param name="id">Employee to delete</param>
        /// <returns>API result</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var connectionString = _config.GetConnectionString("DefaultConnection");
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand();
            
            command.Connection = connection;
            command.CommandText = "DELETE FROM dbo.Employee WHERE EmployeeId = @Id";
            command.Parameters.AddWithValue("@Id", id);
            
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            
            return new JsonResult(new { Message = "Employee deleted successfully" })
            { StatusCode = StatusCodes.Status200OK };
        }

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            var file = Request.Form.Files[0];
            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName?.Trim('"');
            if (fileName == null)
                return new JsonResult(new { Message = "File name is null" }) { StatusCode = StatusCodes.Status400BadRequest };
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Photos", fileName);
            using var stream = new FileStream(path, FileMode.Create);
            file.CopyTo(stream);
            return new JsonResult(new { Message = "File uploaded successfully" })
            { StatusCode = StatusCodes.Status200OK };
        }

        [Route("GetAllDepartmentNames")]
        [HttpGet]
        public JsonResult GetAllDepartmentNames()
        {
            var connectionString = _config.GetConnectionString("DefaultConnection");
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand();

            command.Connection = connection;
            command.CommandText = "SELECT DepartmentName FROM dbo.Department";
            connection.Open();
            var reader = command.ExecuteReader();
            var table = new DataTable();
            table.Load(reader);
            reader.Close();
            connection.Close();

            return new JsonResult(table) { StatusCode = StatusCodes.Status200OK };
        }
    }
}
