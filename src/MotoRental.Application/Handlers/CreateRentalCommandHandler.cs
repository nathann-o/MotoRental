using System;
using System.Threading;
using System.Threading.Tasks;
using MotoRental.Application.DTOs;

namespace MotoRental.Application.Handlers
{
    public class CreateRentalCommandHandler
    {
        private readonly IRiderRepository _riderRepository;
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IRentalRepository _rentalRepository;
        private readonly IRentalDomainService _rentalDomainService;

        public CreateRentalCommandHandler(
            IRiderRepository riderRepository,
            IMotorcycleRepository motorcycleRepository,
            IRentalRepository rentalRepository,
            IRentalDomainService rentalDomainService)
        {
            _riderRepository = riderRepository;
            _motorcycleRepository = motorcycleRepository;
            _rentalRepository = rentalRepository;
            _rentalDomainService = rentalDomainService;
        }

        public async Task<RentalDto> HandleAsync(RentalCreateDto dto, CancellationToken ct = default)
        {
            // basic checks
            var rider = await _riderRepository.GetByIdAsync(dto.RiderId, ct);
            if (rider == null) throw new DomainException("Rider not found.");
            if (!rider.HasCategoryA()) throw new DomainException("Rider must have CNH category A to rent.");

            var motorcycle = await _motorcycleRepository.GetByIdAsync(dto.MotorcycleId, ct);
            if (motorcycle == null) throw new DomainException("Motorcycle not found.");

            // check availability (no active rental overlapping)
            var start = dto.CreatedAt.Date.AddDays(1); // requirement: start = created + 1
            var isAvailable = await _rentalRepository.IsMotorcycleAvailableAsync(dto.MotorcycleId, start, ct);
            if (!isAvailable) throw new DomainException("Motorcycle not available for the selected start date.");

            // map plan (validate allowed values)
            var plan = dto.PlanDays switch
            {
                7 => Plan.Days7,
                15 => Plan.Days15,
                30 => Plan.Days30,
                45 => Plan.Days45,
                50 => Plan.Days50,
                _ => throw new DomainException("Invalid plan selected.")
            };

            var rental = Rental.Create(Guid.Empty, dto.RiderId, dto.MotorcycleId, plan, dto.CreatedAt, dto.EndDate, dto.ExpectedEndDate);

            await _rentalRepository.AddAsync(rental, ct);
            await _rentalRepository.SaveChangesAsync(ct);

            return new RentalDto
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
        }
    }
}
