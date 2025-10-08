public interface IRiderRepository
{
    Task AddAsync(Rider rider, CancellationToken ct = default);
    Task<Rider> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<Rider> GetByCnpjAsync(string cnpj, CancellationToken ct = default);
    Task<Rider> GetByCnhNumberAsync(string cnhNumber, CancellationToken ct = default);
    Task<bool> ExistsByCnpjAsync(string cnpj, CancellationToken ct = default);
    Task<bool> ExistsByCnhNumberAsync(string cnhNumber, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}
