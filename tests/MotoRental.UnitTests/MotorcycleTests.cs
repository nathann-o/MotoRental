public class MotorcycleTests
{
    [Fact]
    public void RegisterMotorcycle_ShouldRaiseMotorcycleRegisteredEvent()
    {
        var id = Guid.NewGuid();
        var plate = Plate.Create("ABC1D234");
        var moto = Motorcycle.Register(id, 2024, "Honda CG", plate);

        Assert.Single(moto.DomainEvents);
        Assert.IsType<MotorcycleRegisteredEvent>(moto.DomainEvents.First());
    }

    [Fact]
    public void UpdatePlate_ShouldChangePlate()
    {
        var plate = Plate.Create("AAA1111");
        var m = Motorcycle.Register(Guid.NewGuid(), 2023, "Model X", plate);

        var newPlate = Plate.Create("BBB2222");
        m.UpdatePlate(newPlate);

        Assert.Equal("BBB2222", m.Plate.Value);
    }
}
