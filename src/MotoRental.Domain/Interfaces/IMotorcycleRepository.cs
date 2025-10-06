public interface IMotorcycleRepository
{
    Task AddAsync(Rental rental, CancellationToken ct = default);
    Task<Rental> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<Motorcycle> GetByPlateAsync(string plate, CancellationToken ct = default);
    Task<IEnumerable<Motorcycle>> ListAsync(string plateFilter = null, CancellationToken ct = default);
    Task RemoveAsync(Motorcycle motorcycle, CancellationToken ct = default);
    Task<bool> ExistsByPlateAsync(string plate, CancellationToken ct = default);
}
