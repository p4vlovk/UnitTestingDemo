namespace DemoCode.Tests;

using AutoFixture;
using Xunit;

public class BookingShould
{
    [Fact]
    public void CalculateTotalFlightTime()
    {
        var fixture = new Fixture();
        fixture.Inject(new AirportCode("LHR"));
        var sut = fixture.Create<Booking>();

        // etc.
    }
}