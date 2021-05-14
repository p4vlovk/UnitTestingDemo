namespace DemoCode
{
    using System;

    public class FlightDetails2
    {
        public FlightDetails2(AirportCode departureAirportCode, AirportCode arrivalAirportCode)
        {
            this.DepartureAirportCode = departureAirportCode;
            this.ArrivalAirportCode = arrivalAirportCode;
        }

        public AirportCode DepartureAirportCode { get; }

        public AirportCode ArrivalAirportCode { get; }

        public TimeSpan FlightDuration { get; set; }

        public string AirlineName { get; set; }
    }
}
