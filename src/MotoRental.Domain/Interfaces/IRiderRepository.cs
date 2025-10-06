public interface IRiderRepository
{
    Task AddAsync(Rider rider, CancellationToken ct = default);
    //CNH photo implementation
}
