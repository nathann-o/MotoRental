using MotoRental.Application.DTOs;

namespace MotoRental.Application.Handlers
{
    public class RegisterMotorcycleCommandHandler
    {
        private readonly IMotorcycleRepository _motorcycleRepository;

        public RegisterMotorcycleCommandHandler(IMotorcycleRepository motorcycleRepository)
        {
            _motorcycleRepository = motorcycleRepository;
        }

        public async Task<MotorcycleDto> HandleAsync(MotorcycleCreateDto dto, CancellationToken ct = default)
        {
            // check duplicate plate
            var plateNormalized = dto.Plate.Trim().ToUpperInvariant();
            if (await _motorcycleRepository.ExistsByPlateAsync(plateNormalized, ct))
                throw new DomainException("Plate already registered.");

            // create VO and entity
            var plateVo = Plate.Create(dto.Plate);
            var motorcycle = Motorcycle.Register(Guid.Empty, dto.Year, dto.Model, plateVo);

            await _motorcycleRepository.AddAsync(motorcycle, ct);
            await _motorcycleRepository.SaveChangesAsync(ct);

            return new MotorcycleDto
            {
                Id = motorcycle.Id,
                Year = motorcycle.Year,
                Model = motorcycle.Model,
                Plate = motorcycle.Plate.Value
            };
        }
    }
}
