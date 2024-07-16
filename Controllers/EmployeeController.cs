using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MONGO_DB_API.Models.Entities;
using MONGO_DB_API.Services.Interfaces;
using MONGO_DB_API.Models.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace MONGO_DB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        [Route("GetAllEmployees")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllAsync();
            return Ok(employees);
        }

        [Authorize]
        [HttpGet]
        [Route("GetEmployeeById")]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeById(string id)
        {
            if (!ObjectId.TryParse(id, out var objectId))
            {
                return BadRequest("Invalid ObjectId format");
            }

            var employee = await _employeeService.GetByIdAsync(objectId);

            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        //[Authorize]
        [HttpPost]
        [Route("CreateEmployee")]
        public async Task<ActionResult<EmployeeDto>> CreateEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdEmployee = await _employeeService.CreateAsync(employee);

            return CreatedAtAction(nameof(GetEmployeeById), new { id = createdEmployee.Id.ToString() }, createdEmployee);
        }

        [Authorize]
        [HttpPut]
        [Route("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee(string id, Employee employee)
        {
            if (!ObjectId.TryParse(id, out var objectId))
            {
                return BadRequest("Invalid ObjectId format");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingEmployee = await _employeeService.GetByIdAsync(objectId);
            if (existingEmployee == null)
            {
                return NotFound();
            }
            employee.Id = objectId;
            await _employeeService.UpdateAsync(objectId, employee);

            return NoContent();
        }

        [Authorize]
        [HttpDelete]
        [Route("DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            if (!ObjectId.TryParse(id, out var objectId))
            {
                return BadRequest("Invalid ObjectId format");
            }

            var existingEmployee = await _employeeService.GetByIdAsync(objectId);
            if (existingEmployee == null)
            {
                return NotFound();
            }

            await _employeeService.DeleteAsync(objectId);

            return NoContent();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDto data)
        {
            _logger.LogInformation("Received request to login employees: {data.Email}", data.Email);

            try
            {
                var result = await _employeeService.LoginAsync(data);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while login employees: {data.Email}", data.Email);
                return StatusCode(500, "Internal server error");
            }

        }

        [Authorize]
        [HttpGet]
        [Route("GetEmployeeByDepartment")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployeeByDepartment(string position)
        {
            _logger.LogInformation("Received request to fetch employees by position: {Position}", position);

            try
            {
                var employees = await _employeeService.GetEmployeeByDepartmentAsync(position);

                return Ok(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching employees by position: {Position}", position);
                return StatusCode(500, "Internal server error");
            }
      
        }
    
    }
}
