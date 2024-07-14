using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MONGO_DB_API.Models.Entities;
using MONGO_DB_API.Services.Interfaces;
using MongoDB.Bson;

namespace MONGO_DB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly IPositionService _PositionService;

        public PositionController(IPositionService PositionService)
        {
            _PositionService = PositionService;
        }

        [HttpGet]
        [Route("GetAllPositions")]
        public async Task<ActionResult<IEnumerable<Position>>> GetAllPositions()
        {
            var Positions = await _PositionService.GetAllAsync();
            return Ok(Positions);
        }

        [HttpGet]
        [Route("GetPositionById")]
        public async Task<ActionResult<Position>> GetPositionById(string id)
        {
            if (!ObjectId.TryParse(id, out var objectId))
            {
                return BadRequest("Invalid ObjectId format");
            }

            var Position = await _PositionService.GetByIdAsync(objectId);

            if (Position == null)
            {
                return NotFound();
            }

            return Ok(Position);
        }

        [HttpPost]
        [Route("CreatePosition")]
        public async Task<ActionResult<Position>> CreatePosition(Position Position)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdPosition = await _PositionService.CreateAsync(Position);

            return CreatedAtAction(nameof(GetPositionById), new { id = createdPosition.Id.ToString() }, createdPosition);
        }

        [HttpPut]
        [Route("UpdatePosition")]
        public async Task<IActionResult> UpdatePosition(string id, Position Position)
        {
            if (!ObjectId.TryParse(id, out var objectId))
            {
                return BadRequest("Invalid ObjectId format");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingPosition = await _PositionService.GetByIdAsync(objectId);
            if (existingPosition == null)
            {
                return NotFound();
            }

            Position.Id = objectId;
            await _PositionService.UpdateAsync(objectId, Position);

            return NoContent();
        }

        [HttpDelete]
        [Route("DeletePosition")]
        public async Task<IActionResult> DeletePosition(string id)
        {
            if (!ObjectId.TryParse(id, out var objectId))
            {
                return BadRequest("Invalid ObjectId format");
            }

            var existingPosition = await _PositionService.GetByIdAsync(objectId);
            if (existingPosition == null)
            {
                return NotFound();
            }

            await _PositionService.DeleteAsync(objectId);

            return NoContent();
        }
    }
}
