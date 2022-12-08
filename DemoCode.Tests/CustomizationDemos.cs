namespace DemoCode.Tests;

using System;
using AutoFixture;
using DemoCode.Tests.SpecimenBuilders;
using Xunit;

public class CustomizationDemos
{
    private readonly Fixture fixture;

    public CustomizationDemos() => this.fixture = new Fixture();
    
    [Fact]
    public void DateTimeCustomization()
    {
        //fixture.Customize(new CurrentDateTimeCustomization());
        this.fixture.Customizations.Add(new CurrentDateTimeGenerator());
        var date1 = this.fixture.Create<DateTime>();
        var date2 = this.fixture.Create<DateTime>();

        // etc.
    }

    [Fact]
    public void CustomizedPipeline()
    {
        this.fixture.Customizations.Add(new AirportCodeStringGenerator());
        var flight = this.fixture.Create<FlightDetails>();
        var airport = this.fixture.Create<Airport>();

        // etc.
    }
        
    [Fact]
    public void CustomizedPipeline2()
    {
        this.fixture.Customizations.Add(new AirportCodeGenerator());
        var flight = this.fixture.Create<FlightDetails2>();
        var airport = this.fixture.Create<Airport2>();

        // etc.
    }
}