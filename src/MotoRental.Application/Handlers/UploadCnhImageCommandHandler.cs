using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MotoRental.Application.DTOs;
using MotoRental.Domain.Interfaces;

namespace MotoRental.Application.Handlers
{
    public class UploadCnhImageCommandHandler
    {
        private readonly IRiderRepository _riderRepository;
        private readonly IStorageService _storageService;

        public UploadCnhImageCommandHandler(IRiderRepository riderRepository, IStorageService storageService)
        {
            _riderRepository = riderRepository;
            _storageService = storageService;
        }

        public async Task<string> HandleAsync(Guid riderId, Stream imageStream, string fileName, string contentType, CancellationToken ct = default)
        {
            var rider = await _riderRepository.GetByIdAsync(riderId, ct);
            if (rider == null) throw new DomainException("Rider not found.");

            var ext = Path.GetExtension(fileName).ToLowerInvariant();
            if (ext != ".png" && ext != ".bmp" && contentType != "image/png" && contentType != "image/bmp")
                throw new DomainException("Only PNG or BMP images are accepted.");

            var finalName = $"{Guid.NewGuid()}{ext}";
            var container = "riders-cnh";

            var url = await _storageService.SaveAsync(container, imageStream, finalName, ct);

            rider.UpdateCnhImage(url);
            await _riderRepository.SaveChangesAsync(ct);

            return url;
        }
    }
}
