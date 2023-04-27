namespace DemoCode;

using System;
using System.Collections.Generic;

public sealed class AirportCode : ValueObject
{
    private readonly string value;

    public AirportCode(string value)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));

        if (!IsValid(value))
            throw new FormatException($"Airport code {nameof(value)} is not in the correct format. Airport codes should be three uppercase letters.");

        this.value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.value;
    }
    
    public override string ToString() => this.value;

    public static implicit operator string(AirportCode code) => code.value;

    public static explicit operator AirportCode(string value) => new(value);

    private static bool IsValid(string airportCode)
    {
        bool isCorrectLength = airportCode.Length == 3;
        bool isUpperCase = airportCode == airportCode.ToUpperInvariant();

        return isCorrectLength && isUpperCase;
    }
}