using MotoRental.Application.DTOs;
using MotoRental.Application.Messaging;
using System.Reflection;

namespace MotoRental.Application.Handlers
{
    public class RegisterMotorcycleCommandHandler
    {
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IRabbitMqPublisher _publisher;

        public RegisterMotorcycleCommandHandler(IMotorcycleRepository motorcycleRepository, IRabbitMqPublisher publisher)
        {
            _motorcycleRepository = motorcycleRepository;
            _publisher = publisher;
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

            var motoNew = new MotorcycleDto
            {
                Id = motorcycle.Id,
                Year = motorcycle.Year,
                Model = motorcycle.Model,
                Plate = motorcycle.Plate.Value
            };

            await _publisher.PublishMotorcycleCreatedAsync(motoNew);

            return motoNew;


        }
    }
}
