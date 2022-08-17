namespace DemoCode
{
    using System;

    public class Airport
    {
        private string code;

        public string AirportCode
        {
            get => this.code;
            set
            {
                EnsureValidAirportCode(value);
                this.code = value;
            }
        }

        public string AirportName { get; set; }

        private static void EnsureValidAirportCode(string airportCode)
        {
            var isWrongLength = airportCode.Length != 3;
            var isWrongCase = airportCode != airportCode.ToUpperInvariant();
            if (isWrongLength || isWrongCase)
            {
                throw new Exception($"{airportCode} is an invalid airport.");
            }
        }
    }

    public class Airport2
    {
        public AirportCode AirportCode { get; set; }
        
        public string AirportName { get; set; }
    }
}
