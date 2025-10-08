using Microsoft.EntityFrameworkCore;
using MotoRental.Infrastructure.Persistence;

namespace MotoRental.Infrastructure.Repositories;

public class MotorcycleRepository : IMotorcycleRepository
{
    private readonly AppDbContext _context;
    public MotorcycleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Motorcycle moto, CancellationToken ct = default)
    {
        await _context.Motorcycles.AddAsync(moto, ct);
    }

    public async Task<Motorcycle> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _context.Motorcycles
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id, ct);
    }

    public async Task<Motorcycle> GetByPlateAsync(string plate, CancellationToken ct = default)
    {
        return await _context.Motorcycles
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Plate.Value == plate, ct);
    }

    public async Task<IEnumerable<Motorcycle>> ListAsync(string plateFilter = null, CancellationToken ct = default)
    {
        var query = _context.Motorcycles.AsQueryable();

        if (!string.IsNullOrWhiteSpace(plateFilter))
            query = query.Where(m => m.Plate.Value.Contains(plateFilter));

        return await query.AsNoTracking().ToListAsync(ct);
    }

    public async Task RemoveAsync(Motorcycle moto, CancellationToken ct = default)
    {
        _context.Motorcycles.Remove(moto);
        await Task.CompletedTask;
    }

    public async Task<bool> ExistsByPlateAsync(string plate, CancellationToken ct = default)
    {
        return await _context.Motorcycles.AnyAsync(m => m.Plate.Value == plate, ct);
    }

    public async Task SaveChangesAsync(CancellationToken ct = default)
    {
        await _context.SaveChangesAsync(ct);
    }
}
