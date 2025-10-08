using System;
using System.Threading;
using System.Threading.Tasks;
using MotoRental.Application.DTOs;


namespace MotoRental.Application.Handlers
{
    public class RegisterRiderCommandHandler
    {
        private readonly IRiderRepository _riderRepository;

        public RegisterRiderCommandHandler(IRiderRepository riderRepository)
        {
            _riderRepository = riderRepository;
        }

        public async Task<RiderDto> HandleAsync(RiderCreateDto dto, CancellationToken ct = default)
        {
            // uniqueness checks
            var cnpjNormalized = dto.Cnpj.Trim();
            if (await _riderRepository.ExistsByCnpjAsync(cnpjNormalized, ct))
                throw new DomainException("CNPJ already registered.");

            var cnhNormalized = dto.CnhNumber.Trim();
            if (await _riderRepository.ExistsByCnhNumberAsync(cnhNormalized, ct))
                throw new DomainException("CNH number already registered.");

            // map CNH type
            var cnhType = dto.CnhType.ToUpperInvariant() switch
            {
                "A" => CnhType.A,
                "B" => CnhType.B,
                "AB" => CnhType.AB,
                _ => throw new DomainException("Invalid CNH type.")
            };

            var rider = Rider.Create(Guid.Empty, dto.Name, cnpjNormalized, dto.BirthDate, cnhNormalized, cnhType);

            await _riderRepository.AddAsync(rider, ct);
            await _riderRepository.SaveChangesAsync(ct);

            return new RiderDto
            {
                Id = rider.Id,
                Name = rider.Name,
                Cnpj = rider.Cnpj,
                BirthDate = rider.BirthDate,
                CnhNumber = rider.CnhNumber,
                CnhType = rider.CnhType.ToString(),
                CnhImageUrl = rider.CnhImageUrl
            };
        }
    }
}
