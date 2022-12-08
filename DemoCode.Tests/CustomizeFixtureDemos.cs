namespace DemoCode.Tests;

using System;
using AutoFixture;
using Xunit;

public class CustomizeFixtureDemos
{
    private readonly Fixture fixture = new();
    
    [Fact]
    public void Error()
    {
        this.fixture.Inject("LHR");
        var flight = this.fixture.Create<FlightDetails>();
    }

    [Fact]
    public void SettingValueForCustomType()
    {
        this.fixture.Inject(new FlightDetails
        {
            DepartureAirportCode = "PER",
            ArrivalAirportCode = "LHR",
            FlightDuration = TimeSpan.FromHours(10),
            AirlineName = "Awesome Aero"
        });

        var flight1 = this.fixture.Create<FlightDetails>();
        var flight2 = this.fixture.Create<FlightDetails>();
    }

    [Fact]
    public void CustomCreationFunction()
    {
        this.fixture.Register(() => DateTime.Now.Ticks.ToString());
        var string1 = this.fixture.Create<string>();
        var string2 = this.fixture.Create<string>();
    }

    [Fact]
    public void FreezingValues()
    {
        //var id = fixture.Create<int>();
        //fixture.Inject(id);
        //var customerName = fixture.Create<string>();
        //fixture.Inject(customerName);
        var id = this.fixture.Freeze<int>();
        var customerName = this.fixture.Freeze<string>();
        var sut = this.fixture.Create<Order>();

        Assert.Equal($"{id}-{customerName}", sut.ToString());
    }

    // Test Data Builder Pattern enables fine-grained control over how objects of specific types are created
    // Test Data Builder Pattern used for creating a specific instance of a type
    [Fact]
    public void OmitSettingSpecificProperties()
    {
        var flight = this.fixture.Build<FlightDetails>()
            .Without(x => x.ArrivalAirportCode)
            .Without(x => x.DepartureAirportCode)
            .Create();
    }

    [Fact]
    public void OmitSettingAllProperties()
    {
        var flight = this.fixture.Build<FlightDetails>()
            .OmitAutoProperties()
            .Create();
    }

    [Fact]
    public void CustomizedBuilding()
    {
        var flight = this.fixture.Build<FlightDetails>()
            .With(x => x.ArrivalAirportCode, "LAX")
            .With(x => x.DepartureAirportCode, "LHR")
            .Create();
    }

    [Fact]
    public void CustomizedBuildingWithActions()
    {
        var flight = this.fixture.Build<FlightDetails>()
            .With(x => x.ArrivalAirportCode, "LAX")
            .With(x => x.DepartureAirportCode, "LHR")
            .Without(x => x.MealOptions)
            .Do(x => x.MealOptions.Add("Chicken"))
            .Do(x => x.MealOptions.Add("Fish"))
            .Create();
    }

    // Test Data Builder Pattern used for specifying how to create all instances of a type
    [Fact]
    public void CustomizedBuildingForATypeInFixture()
    {
        this.fixture.Customize<FlightDetails>(fd =>
            fd.With(x => x.DepartureAirportCode, "LHR")
                .With(x => x.ArrivalAirportCode, "LAX")
                .With(x => x.AirlineName, "Fly Fly Premium Air")
                .Without(x => x.MealOptions)
                .Do(x => x.MealOptions.Add("Chicken"))
                .Do(x => x.MealOptions.Add("Fish"))); // no .Create() is required here

        var flight1 = this.fixture.Create<FlightDetails>();
        var flight2 = this.fixture.Create<FlightDetails>();
    }
}