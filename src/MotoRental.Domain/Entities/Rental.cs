using System;

public sealed class Rental : AggregateRoot
{
    public Guid Id { get; private set; }
    public Guid RiderId { get; private set; }
    public Guid MotorcycleId { get; private set; }
    public Plan Plan { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime ExpectedEndDate { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public bool IsActive { get; private set; }

    private Rental() { }

    public static Rental Create(Guid id, Guid riderId, Guid motorcycleId, Plan plan, DateTime createdAt)
    {
        if (id == Guid.Empty) id = Guid.NewGuid();
        var start = createdAt.Date.AddDays(1);
        var expected = start.AddDays((int)plan);

        return new Rental
        {
            Id = id,
            RiderId = riderId,
            MotorcycleId = motorcycleId,
            Plan = plan,
            CreatedAt = createdAt,
            StartDate = start,
            ExpectedEndDate = expected,
            IsActive = true
        };
    }

    public void Close(DateTime actualReturnDate) => IsActive = false;

}
