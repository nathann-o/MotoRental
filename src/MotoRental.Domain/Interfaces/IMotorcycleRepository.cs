public interface IMotorcycleRepository
{
    Task AddAsync(Motorcycle moto, CancellationToken ct = default);
    Task<Motorcycle> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<Motorcycle> GetByPlateAsync(string plate, CancellationToken ct = default);
    Task<IEnumerable<Motorcycle>> ListAsync(string plateFilter = null, CancellationToken ct = default);
    Task RemoveAsync(Motorcycle moto, CancellationToken ct = default);
    Task<bool> ExistsByPlateAsync(string plate, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}
