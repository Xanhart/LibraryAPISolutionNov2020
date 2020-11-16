

using LibraryAPI.Models.Employees;
using LibraryAPI.Models.Status;
using LibraryAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

namespace LibraryAPI.Controllers
{

    public class DemoController : ControllerBase
    {
        private readonly IProvideServerStatusInformation _statusService;

        public DemoController(IProvideServerStatusInformation statusService)
        {
            _statusService = statusService;
        }



        // GET /status
        [HttpGet("/status")]
        public ActionResult GetTheStatus()
        {
            GetStatusResponse response = null;
            // WTCYWYH - Write the code you wish you had.
            try
            {
                response = _statusService.GetCurrentStatus();
            }
            catch (Exception)
            {

                response = new GetStatusResponse
                {
                    Message = "Status is unavailable",
                    CreatedAt = new DateTime() // January 1, year 1 at midnight.
                };
            }
            return Ok(response);
        }
        // GET /employees/99
        [HttpGet("/employees/{employeeId:int}", Name ="demo-getemployee")]
        public ActionResult GetEmployee(int employeeId)
        {
            return Ok(new { EmployeId = employeeId, Name = "Bob Smith" });
        }

        [HttpGet("/blogs/{year:int}/{month:int}/{day:int}")]
        public ActionResult GetBlogPosts(int year, int month, int day)
        {
            return Ok($"Getting the blog posts for {month}/{day}/{year}");
        }


        
        // GET /agents
        // GET /agents?state=CO
        [HttpGet("/agents")]
        public ActionResult GetAgents([FromQuery] string state = "All", [FromQuery] string city="All")
        {
            return Ok($"Getting Agents from State {state} and city {city}");
        }


        [HttpGet("/whoami")] // User-Agent
        public ActionResult GetUserAgent([FromHeader(Name ="User-Agent")] string userAgent)
        {
            return Ok($"I see you are running {userAgent}");
        }

        [HttpPost("/employees")]
        public ActionResult Hire([FromBody] PostEmployeeRequest employeeToHire)
        {
            // POSTing To a Collection
            // 1. Validate it. If Not, return a 400. (we'll do this tomorrow)
            // 2. Change the world, man. Add it to the database, whever hiring someone means to you.
            var response = new GetEmployeeDetailsResponse
            {
                Id = new Random().Next(40, 2000),
                Name = employeeToHire.Name,
                Department = employeeToHire.Department,
                Manager = "Sue Jones",
                Salary = employeeToHire.StartingSalary * 1.3M
            };

            // Return:
            //   - At least a 200, but 201 is "More" Correct (201 means 'created')
            //   - a birth announcement - Location: http://localhost:1337/employees/500
            //   - Also send them a copy of the newly created entity.
            return CreatedAtRoute("demo-getemployee", new { employeeId = response.Id }, response);
        }
    }
}

