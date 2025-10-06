using System;

public sealed class Rider : AggregateRoot
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Cnpj { get; private set; }
    public DateTime BirthDate { get; private set; }
    public string CnhNumber { get; private set; }
    public CnhType CnhType { get; private set; }
    public string CnhImageUrl { get; private set; }

    private Rider() { }

    public static Rider Create(Guid id, string name, string cnpj, DateTime birthDate, string cnhNumber, CnhType cnhType)
    {
        if (id == Guid.Empty) id = Guid.NewGuid();
        if (string.IsNullOrWhiteSpace(name)) throw new DomainException("Name is required.");
        if (string.IsNullOrWhiteSpace(cnpj)) throw new DomainException("CNPJ is required.");
        if (string.IsNullOrWhiteSpace(cnhNumber)) throw new DomainException("CNH number is required.");

        return new Rider
        {
            Id = id,
            Name = name.Trim(),
            Cnpj = cnpj.Trim(),
            BirthDate = birthDate,
            CnhNumber = cnhNumber.Trim(),
            CnhType = cnhType
        };
    }

    public void UpdateCnhImage(string url)
    {
        if (string.IsNullOrWhiteSpace(url)) throw new DomainException("Image URL is required.");
        CnhImageUrl = url;
    }

    public bool HasCategoryA() => CnhType == CnhType.A || CnhType == CnhType.AB;
}
