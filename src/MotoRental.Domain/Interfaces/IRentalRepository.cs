public interface IRentalRepository
{
    Task AddAsync(Rental rental, CancellationToken ct = default);
    Task<Rental> GetByIdAsync(Guid id, CancellationToken ct = default);
    //Devolution implementation
}
