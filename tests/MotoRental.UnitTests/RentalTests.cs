public class RentalTests
{
    [Fact]
    public void CreateRental_StartDateIsCreationPlusOne()
    {
        var now = new DateTime(2025, 10, 01);
        var rental = Rental.Create(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Plan.Days7, now);

        Assert.Equal(now.Date.AddDays(1), rental.StartDate.Date);
    }
}