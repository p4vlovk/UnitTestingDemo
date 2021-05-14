namespace DemoCode.Tests
{
    using System;
    using AutoFixture;
    using DemoCode.Tests.SpecimenBuilders;
    using Xunit;

    public class CustomizationDemos
    {
        [Fact]
        public void DateTimeCustomization()
        {
            var fixture = new Fixture();
            //fixture.Customize(new CurrentDateTimeCustomization());
            fixture.Customizations.Add(new CurrentDateTimeGenerator());
            var date1 = fixture.Create<DateTime>();
            var date2 = fixture.Create<DateTime>();

            // etc.
        }

        [Fact]
        public void CustomizedPipeline()
        {
            var fixture = new Fixture();
            fixture.Customizations.Add(new AirportCodeStringPropertyGenerator());
            var flight = fixture.Create<FlightDetails>();
            var airport = fixture.Create<Airport>();

            // etc.
        }
    }
}
