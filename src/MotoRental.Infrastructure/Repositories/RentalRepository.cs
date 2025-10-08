using Microsoft.EntityFrameworkCore;
using MotoRental.Infrastructure.Persistence;

namespace MotoRental.Infrastructure.Repositories;

public class RentalRepository : IRentalRepository
{
    private readonly AppDbContext _context;
    public RentalRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Rental rental, CancellationToken ct = default)
    {
        await _context.Rentals.AddAsync(rental, ct);
    }

    public async Task<Rental> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _context.Rentals
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id, ct);
    }

    public async Task<Rental> GetActiveByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _context.Rentals
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id && r.ExpectedEndDate > DateTime.Now, ct);
    }

    public async Task<bool> HasAnyRentalForMotorcycleAsync(Guid motorcycleId, CancellationToken ct = default)
    {
        return await _context.Rentals.AnyAsync(r => r.MotorcycleId == motorcycleId, ct);
    }

    public async Task<bool> IsMotorcycleAvailableAsync(Guid motorcycleId, DateTime startDate, CancellationToken ct = default)
    {
        return !await _context.Rentals
            .AnyAsync(r => r.MotorcycleId == motorcycleId &&
                           r.StartDate <= startDate &&
                           r.ExpectedEndDate >= startDate, ct);
    }

    public async Task SaveChangesAsync(CancellationToken ct = default)
    {
        await _context.SaveChangesAsync(ct);
    }
}
