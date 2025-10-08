public interface IRentalRepository
{
    Task AddAsync(Rental rental, CancellationToken ct = default);
    Task<Rental> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<bool> HasAnyRentalForMotorcycleAsync(Guid motorcycleId, CancellationToken ct = default);
    Task<Rental> GetActiveByIdAsync(Guid id, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
    Task<bool> IsMotorcycleAvailableAsync(Guid motorcycleId, DateTime startDate, CancellationToken ct = default);
}
