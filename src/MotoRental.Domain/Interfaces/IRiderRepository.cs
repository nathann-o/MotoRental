public interface IRiderRepository
{
    Task AddAsync(Rider rider, CancellationToken ct = default);
    Task<Rider> GetByIdAsync(Guid riderId, CancellationToken ct = default);
    //CNH photo implementation
}
