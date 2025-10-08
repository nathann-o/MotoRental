using Microsoft.AspNetCore.Mvc;
using MotoRental.Application.DTOs;
using MotoRental.Application.Handlers;

namespace MotoRental.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MotorcyclesController : ControllerBase
    {
        private readonly RegisterMotorcycleCommandHandler _registerHandler;
        private readonly GetMotorcyclesQueryHandler _getHandler;
        private readonly UpdateMotorcyclePlateCommandHandler _updateHandler;
        private readonly DeleteMotorcycleCommandHandler _deleteHandler;
        private readonly IMotorcycleRepository _motorcycleRepository;


        public MotorcyclesController(
        RegisterMotorcycleCommandHandler registerHandler,
        GetMotorcyclesQueryHandler getHandler,
        UpdateMotorcyclePlateCommandHandler updateHandler,
        DeleteMotorcycleCommandHandler deleteHandler,
        IMotorcycleRepository motorcycleRepository)
        {
            _registerHandler = registerHandler;
            _getHandler = getHandler;
            _updateHandler = updateHandler;
            _deleteHandler = deleteHandler;
            _motorcycleRepository = motorcycleRepository;
        }


        /// <summary>
        /// Register a new motorcycle
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MotorcycleCreateDto dto)
        {
            var result = await _registerHandler.HandleAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }


        /// <summary>
        /// Get motorcycles (optional plate filter)
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? plate)
        {
            var result = await _getHandler.HandleAsync(plate);
            return Ok(result);
        }


        /// <summary>
        /// Get motorcycle by id
        /// </summary>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var m = await _motorcycleRepository.GetByIdAsync(id);
            if (m == null) return NotFound();


            var dto = new MotorcycleDto
            {
                Id = m.Id,
                Year = m.Year,
                Model = m.Model,
                Plate = m.Plate.Value
            };
            return Ok(dto);
        }

        /// <summary>
        /// Update motorcycle plate
        /// </summary>
        [HttpPut("{id:guid}/plate")]
        public async Task<IActionResult> UpdatePlate(Guid id, [FromBody] UpdatePlateDto dto)
        {
            var result = await _updateHandler.HandleAsync(id, dto.Plate);
            return Ok(result);
        }


        /// <summary>
        /// Delete motorcycle (only if no rentals exist)
        /// </summary>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _deleteHandler.HandleAsync(id);
            return NoContent();
        }

        public record UpdatePlateDto(string Plate);
    }
}