using System;
using System.Threading;
using System.Threading.Tasks;
using MotoRental.Domain.Interfaces;

namespace MotoRental.Application.Handlers
{
    public class DeleteMotorcycleCommandHandler
    {
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IRentalRepository _rentalRepository;

        public DeleteMotorcycleCommandHandler(IMotorcycleRepository motorcycleRepository, IRentalRepository rentalRepository)
        {
            _motorcycleRepository = motorcycleRepository;
            _rentalRepository = rentalRepository;
        }

        public async Task HandleAsync(Guid motorcycleId, CancellationToken ct = default)
        {
            var moto = await _motorcycleRepository.GetByIdAsync(motorcycleId, ct);
            if (moto is null) throw new DomainException("Motorcycle not found.");

            var hasRental = await _rentalRepository.HasAnyRentalForMotorcycleAsync(motorcycleId, ct);
            if (hasRental) throw new DomainException("Cannot remove motorcycle with rental records.");

            await _motorcycleRepository.RemoveAsync(moto, ct);
            await _motorcycleRepository.SaveChangesAsync(ct);
        }
    }
}
