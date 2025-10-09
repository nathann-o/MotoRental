public class RentalDomainService : IRentalDomainService
{
    private readonly IRiderRepository _riderRepository;

    public RentalDomainService(IRiderRepository riderRepository)
    {
        _riderRepository = riderRepository;
    }

    public async Task<Rental> CreateRentalAsync(Guid riderId, Guid motorcycleId, Plan plan, DateTime now)
    {
        var rider = await _riderRepository.GetByIdAsync(riderId);
        if (rider is null) throw new DomainException("Rider not found");

        if (!rider.HasCategoryA()) throw new DomainException("Rider must have category A to rent a motorcycle.");

        var rental = Rental.Create(Guid.Empty, riderId, motorcycleId, plan, now, now, now);
        return rental;
    }

    public decimal ComputeCharge(Plan plan, DateTime start, DateTime expectedEnd, DateTime actualReturn)
    {
        var daily = plan.DailyRate();
        var plannedDays = (int)plan;

        if (actualReturn.Date < expectedEnd.Date)
        {
            var usedDays = (actualReturn.Date - start.Date).Days + 1;
            var unusedDays = plannedDays - usedDays;
            if (unusedDays < 0) unusedDays = 0;
            var baseCharge = usedDays * daily;
            decimal penaltyPct = plan == Plan.Days7 ? 0.2m : plan == Plan.Days15 ? 0.4m : 0m;
            var penalty = penaltyPct * (unusedDays * daily);
            return baseCharge + penalty;
        }
        else if (actualReturn.Date > expectedEnd.Date)
        {
            var extraDays = (actualReturn.Date - expectedEnd.Date).Days;
            var baseCharge = plannedDays * daily;
            var lateFee = extraDays * 50m;
            return baseCharge + lateFee;
        }
        else
        {
            return plannedDays * daily;
        }
    }
}
