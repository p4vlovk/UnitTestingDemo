namespace DemoCode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Booking
    {
        public string BookingReference { get; set; }

        public string CustomerName { get; set; }

        public List<FlightDetails2> Legs { get; set; } = new List<FlightDetails2>();

        public TimeSpan TotalFlightDuration() =>
            this.Legs.Aggregate(TimeSpan.Zero, (current, flightLeg) => current + flightLeg.FlightDuration);
    }
}
