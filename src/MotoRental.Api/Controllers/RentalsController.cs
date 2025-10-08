using Microsoft.AspNetCore.Mvc;
using MotoRental.Application.DTOs;
using MotoRental.Application.Handlers;
using System;

namespace MotoRental.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentalsController : ControllerBase
    {
        private readonly CreateRentalCommandHandler _createHandler;
        private readonly ReturnRentalCommandHandler _returnHandler;
        private readonly IRentalRepository _rentalRepository;


        public RentalsController(CreateRentalCommandHandler createHandler, ReturnRentalCommandHandler returnHandler, IRentalRepository rentalRepository)
        {
            _createHandler = createHandler;
            _returnHandler = returnHandler;
            _rentalRepository = rentalRepository;
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RentalCreateDto dto)
        {
            var result = await _createHandler.HandleAsync(dto);
            return Created($"/api/rentals/{result.Id}", result);
        }


        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var rental = await _rentalRepository.GetByIdAsync(id);
            if (rental == null) return NotFound();


            var dto = new RentalDto
            {
                Id = rental.Id,
                RiderId = rental.RiderId,
                MotorcycleId = rental.MotorcycleId,
                PlanDays = (int)rental.Plan,
                StartDate = rental.StartDate,
                ExpectedEndDate = rental.ExpectedEndDate,
                CreatedAt = rental.CreatedAt,
                IsActive = rental.IsActive
            };


            return Ok(dto);
        }


        [HttpPut("{id:guid}/return")]
        public async Task<IActionResult> Return(Guid id, [FromBody] RentalReturnRequest request)
        {
            if (id != request.RentalId) return BadRequest(new { message = "Route id and body RentalId must match." });


            var res = await _returnHandler.HandleAsync(new RentalReturnDto { RentalId = request.RentalId, ActualReturnDate = request.ActualReturnDate });
            return Ok(res);
        }

        public record RentalReturnRequest(Guid RentalId, DateTime ActualReturnDate);
    }
}