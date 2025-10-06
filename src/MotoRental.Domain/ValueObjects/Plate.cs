using System;
public sealed record Plate
{
    public string Value { get; }

    private Plate(string value) => Value = value;

    public static Plate Create(string plate)
    {
        if (string.IsNullOrWhiteSpace(plate))
            throw new DomainException("Plate is required.");

        var normalized = plate.Trim().ToUpperInvariant();

        if (normalized.Length < 6 || normalized.Length > 8)
            throw new DomainException("Plate format is invalid.");

        return new Plate(normalized);
    }

    public override string ToString() => Value;
}
