using System;

public sealed class Motorcycle : AggregateRoot
{
    public Guid Id { get; private set; }
    public int Year { get; private set; }
    public string Model { get; private set; }
    public Plate Plate { get; private set; }

    private Motorcycle() { }

    public static Motorcycle Register(Guid id, int year, string model, Plate plate)
    {
        if (id == Guid.Empty) id = Guid.NewGuid();
        if (string.IsNullOrWhiteSpace(model)) throw new DomainException("Model is required.");
        if (year <= 0) throw new DomainException("Year is invalid.");

        var moto = new Motorcycle
        {
            Id = id,
            Year = year,
            Model = model.Trim(),
            Plate = plate
        };

        moto.AddDomainEvent(new MotorcycleRegisteredEvent(moto.Id, moto.Year, moto.Model, moto.Plate.Value));
        return moto;
    }

    public void UpdatePlate(Plate newPlate)
    {
        if (newPlate is null) throw new DomainException("Plate is required.");
        Plate = newPlate;
    }
}
