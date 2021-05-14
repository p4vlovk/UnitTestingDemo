namespace DemoCode
{
    using System;

    public class Airport
    {
        private string _airportCode;

        public string AirportCode
        {
            get => this._airportCode;
            set
            {
                this.EnsureValidAirportCode(value);
                this._airportCode = value;
            }
        }

        public string AirportName { get; set; }

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
