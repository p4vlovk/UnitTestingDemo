namespace DemoCode.Tests;

using System;
using AutoFixture;
using Xunit;

public class CustomizeFixtureDemos
{
    [Fact]
    public void Error()
    {
        var fixture = new Fixture();
        fixture.Inject("LHR");
        var flight = fixture.Create<FlightDetails>();
    }

    [Fact]
    public void SettingValueForCustomType()
    {
        var fixture = new Fixture();
        fixture.Inject(new FlightDetails
        {
            DepartureAirportCode = "PER",
            ArrivalAirportCode = "LHR",
            FlightDuration = TimeSpan.FromHours(10),
            AirlineName = "Awesome Aero"
        });

        var flight1 = fixture.Create<FlightDetails>();
        var flight2 = fixture.Create<FlightDetails>();
    }

    [Fact]
    public void CustomCreationFunction()
    {
        var fixture = new Fixture();
        fixture.Register(() => DateTime.Now.Ticks.ToString());
        var string1 = fixture.Create<string>();
        var string2 = fixture.Create<string>();
    }

    [Fact]
    public void FreezingValues()
    {
        var fixture = new Fixture();
        //var id = fixture.Create<int>();
        //fixture.Inject(id);
        //var customerName = fixture.Create<string>();
        //fixture.Inject(customerName);
        var id = fixture.Freeze<int>();
        var customerName = fixture.Freeze<string>();
        var sut = fixture.Create<Order>();

        Assert.Equal($"{id}-{customerName}", sut.ToString());
    }

    // Test Data Builder Pattern enables fine-grained control over how objects of specific types are created
    // Test Data Builder Pattern used for creating a specific instance of a type
    [Fact]
    public void OmitSettingSpecificProperties()
    {
        var fixture = new Fixture();
        var flight = fixture.Build<FlightDetails>()
            .Without(x => x.ArrivalAirportCode)
            .Without(x => x.DepartureAirportCode)
            .Create();
    }

    [Fact]
    public void OmitSettingAllProperties()
    {
        var fixture = new Fixture();
        var flight = fixture.Build<FlightDetails>()
            .OmitAutoProperties()
            .Create();
    }

    [Fact]
    public void CustomizedBuilding()
    {
        var fixture = new Fixture();
        var flight = fixture.Build<FlightDetails>()
            .With(x => x.ArrivalAirportCode, "LAX")
            .With(x => x.DepartureAirportCode, "LHR")
            .Create();
    }

    [Fact]
    public void CustomizedBuildingWithActions()
    {
        var fixture = new Fixture();
        var flight = fixture.Build<FlightDetails>()
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
        var fixture = new Fixture();
        fixture.Customize<FlightDetails>(fd =>
            fd.With(x => x.DepartureAirportCode, "LHR")
                .With(x => x.ArrivalAirportCode, "LAX")
                .With(x => x.AirlineName, "Fly Fly Premium Air")
                .Without(x => x.MealOptions)
                .Do(x => x.MealOptions.Add("Chicken"))
                .Do(x => x.MealOptions.Add("Fish"))); // no .Create() is required here

        var flight1 = fixture.Create<FlightDetails>();
        var flight2 = fixture.Create<FlightDetails>();
    }
}