namespace DemoCode;

using System;
using System.Collections.Generic;

public class FlightDetails
{
    private readonly string arrivalAirportCode;
    private readonly string departureAirportCode;

    public string DepartureAirportCode
    {
        get => this.departureAirportCode;
        init
        {
            EnsureValidAirportCode(value);
            this.departureAirportCode = value;
        }
    }

    public string ArrivalAirportCode
    {
        get => this.arrivalAirportCode;
        init
        {
            EnsureValidAirportCode(value);
            this.arrivalAirportCode = value;
        }
    }

    public TimeSpan FlightDuration { get; set; }

    public string AirlineName { get; init; }

    public List<string> MealOptions { get; set; } = new();

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