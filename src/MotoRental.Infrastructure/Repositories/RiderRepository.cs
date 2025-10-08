using Microsoft.EntityFrameworkCore;
using MotoRental.Infrastructure.Persistence;

namespace MotoRental.Infrastructure.Repositories;

public class RiderRepository : IRiderRepository
{
    private readonly AppDbContext _context;
    public RiderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Rider rider, CancellationToken ct = default)
    {
        await _context.Riders.AddAsync(rider, ct);
    }

    public async Task<Rider> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _context.Riders
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id, ct);
    }

    public async Task<Rider> GetByCnpjAsync(string cnpj, CancellationToken ct = default)
    {
        return await _context.Riders
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Cnpj == cnpj, ct);
    }

    public async Task<Rider> GetByCnhNumberAsync(string cnhNumber, CancellationToken ct = default)
    {
        return await _context.Riders
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.CnhNumber == cnhNumber, ct);
    }

    public async Task<bool> ExistsByCnpjAsync(string cnpj, CancellationToken ct = default)
    {
        return await _context.Riders.AnyAsync(r => r.Cnpj == cnpj, ct);
    }

    public async Task<bool> ExistsByCnhNumberAsync(string cnhNumber, CancellationToken ct = default)
    {
        return await _context.Riders.AnyAsync(r => r.CnhNumber == cnhNumber, ct);
    }

    public async Task SaveChangesAsync(CancellationToken ct = default)
    {
        await _context.SaveChangesAsync(ct);
    }
}
