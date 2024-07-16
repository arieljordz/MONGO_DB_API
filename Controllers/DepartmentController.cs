using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MONGO_DB_API.Models.Entities;
using MONGO_DB_API.Services.Interfaces;
using MongoDB.Bson;

namespace MONGO_DB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _DepartmentService;

        public DepartmentController(IDepartmentService DepartmentService)
        {
            _DepartmentService = DepartmentService;
        }

        [HttpGet]
        [Route("GetAllDepartments")]
        public async Task<ActionResult<IEnumerable<Department>>> GetAllDepartments()
        {
            var Departments = await _DepartmentService.GetAllAsync();
            return Ok(Departments);
        }

        [HttpGet]
        [Route("GetDepartmentById")]
        public async Task<ActionResult<Department>> GetDepartmentById(string id)
        {
            if (!ObjectId.TryParse(id, out var objectId))
            {
                return BadRequest("Invalid ObjectId format");
            }

            var Department = await _DepartmentService.GetByIdAsync(objectId);

            if (Department == null)
            {
                return NotFound();
            }

            return Ok(Department);
        }

        [HttpPost]
        [Route("CreateDepartment")]
        public async Task<ActionResult<Department>> CreateDepartment(Department Department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdDepartment = await _DepartmentService.CreateAsync(Department);

            return CreatedAtAction(nameof(GetDepartmentById), new { id = createdDepartment.Id.ToString() }, createdDepartment);
        }

        [HttpPut]
        [Route("UpdateDepartment")]
        public async Task<IActionResult> UpdateDepartment(string id, Department Department)
        {
            if (!ObjectId.TryParse(id, out var objectId))
            {
                return BadRequest("Invalid ObjectId format");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingDepartment = await _DepartmentService.GetByIdAsync(objectId);
            if (existingDepartment == null)
            {
                return NotFound();
            }

            Department.Id = objectId;
            await _DepartmentService.UpdateAsync(objectId, Department);

            return NoContent();
        }

        [HttpDelete]
        [Route("DeleteDepartment")]
        public async Task<IActionResult> DeleteDepartment(string id)
        {
            if (!ObjectId.TryParse(id, out var objectId))
            {
                return BadRequest("Invalid ObjectId format");
            }

            var existingDepartment = await _DepartmentService.GetByIdAsync(objectId);
            if (existingDepartment == null)
            {
                return NotFound();
            }

            await _DepartmentService.DeleteAsync(objectId);

            return NoContent();
        }
    }
}
