using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MotoRental.Application.DTOs;
using MotoRental.Domain.Interfaces;

namespace MotoRental.Application.Handlers
{
    public class GetMotorcyclesQueryHandler
    {
        private readonly IMotorcycleRepository _motorcycleRepository;

        public GetMotorcyclesQueryHandler(IMotorcycleRepository motorcycleRepository)
        {
            _motorcycleRepository = motorcycleRepository;
        }

        public async Task<IEnumerable<MotorcycleDto>> HandleAsync(string? plateFilter = null, CancellationToken ct = default)
        {
            var list = await _motorcycleRepository.ListAsync(plateFilter, ct);

            return list.Select(m => new MotorcycleDto
            {
                Id = m.Id,
                Year = m.Year,
                Model = m.Model,
                Plate = m.Plate.Value
            });
        }
    }
}
