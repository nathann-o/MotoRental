using System;
using System.Threading;
using System.Threading.Tasks;
using MotoRental.Application.DTOs;

namespace MotoRental.Application.Handlers
{
    public class UpdateMotorcyclePlateCommandHandler
    {
        private readonly IMotorcycleRepository _motorcycleRepository;

        public UpdateMotorcyclePlateCommandHandler(IMotorcycleRepository motorcycleRepository)
        {
            _motorcycleRepository = motorcycleRepository;
        }

        public async Task<MotorcycleDto> HandleAsync(Guid motorcycleId, string newPlate, CancellationToken ct = default)
        {
            var moto = await _motorcycleRepository.GetByIdAsync(motorcycleId, ct);
            if (moto == null) throw new DomainException("Motorcycle not found.");

            var normalized = newPlate.Trim().ToUpperInvariant();

            var existing = await _motorcycleRepository.GetByPlateAsync(normalized, ct);
            if (existing != null && existing.Id != motorcycleId)
                throw new DomainException("Plate already in use.");

            var plateVo = Plate.Create(newPlate);
            moto.UpdatePlate(plateVo);

            await _motorcycleRepository.SaveChangesAsync(ct);

            return new MotorcycleDto
            {
                Id = moto.Id,
                Year = moto.Year,
                Model = moto.Model,
                Plate = moto.Plate.Value
            };
        }
    }
}
