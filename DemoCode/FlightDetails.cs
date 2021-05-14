namespace DemoCode
{
    using System;
    using System.Collections.Generic;

    public class FlightDetails
    {
        private string arrivalAirportCode;
        private string departureAirportCode;

        public string DepartureAirportCode
        {
            get => this.departureAirportCode;
            set
            {
                this.EnsureValidAirportCode(value);
                this.departureAirportCode = value;
            }
        }

        public string ArrivalAirportCode
        {
            get => this.arrivalAirportCode;
            set
            {
                this.EnsureValidAirportCode(value);
                this.arrivalAirportCode = value;
            }
        }

        public TimeSpan FlightDuration { get; set; }

        public string AirlineName { get; set; }

        public List<string> MealOptions { get; set; } = new List<string>();

        private void EnsureValidAirportCode(string airportCode)
        {
            var isWrongLength = airportCode.Length != 3;
            var isWrongCase = airportCode != airportCode.ToUpperInvariant();
            if (isWrongLength || isWrongCase)
            {
                throw new Exception($"{airportCode} is an invalid airport.");
            }
        }
    }
}
