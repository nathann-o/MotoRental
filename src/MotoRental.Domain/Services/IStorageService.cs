public interface IStorageService
{
    Task<string> SaveAsync(string container, Stream data, string fileName, CancellationToken ct = default);
}
