namespace DemoCode
{
    using System;

    public class AirportCode
    {
        private readonly string code;

        public AirportCode(string code)
        {
            if (code is null)
            {
                throw new ArgumentNullException(nameof(code));
            }

            if (!this.IsValid(code))
            {
                throw new ArgumentException("Airport code should be three uppercase letters.", nameof(code));
            }

            this.code = code;
        }

        public override string ToString() => this.code;

        private bool IsValid(string airportCode)
        {
            var isCorrectLength = airportCode.Length == 3;
            var isUpperCase = airportCode == airportCode.ToUpperInvariant();

            return isCorrectLength && isUpperCase;
        }
    }
}
