public interface IRentalDomainService
{
    Task<Rental> CreateRentalAsync(Guid riderId, Guid motorcycleId, Plan plan, DateTime now);
    decimal ComputeCharge(Plan plan, DateTime start, DateTime expectedEnd, DateTime actualReturn);
}
