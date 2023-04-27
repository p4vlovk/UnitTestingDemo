namespace DemoCode.Tests.SpecimenBuilders;

using System;
using System.Reflection;
using AutoFixture.Kernel;

/* As an alternative, we can create a custom AirportCode class to represent an airport code.
    We can then use this class here to check the type of the property rather than having to
    use reflection to check the property name. */
public class AirportCodeStringGenerator : ISpecimenBuilder
{
    public object Create(object request, ISpecimenContext context)
    {
        // See if we are trying to create a value for a property
        if (request is not PropertyInfo propertyInfo)
            // This specimen does not apply to current request
            return new NoSpecimen(); // null is a valid specimen so return NoSpecimen

        // Now we know we are dealing with a property
        // Are we creating a value for an airport code?
        var isAirportCodeProperty = propertyInfo.Name.Contains("AirportCode");
        var isStringProperty = propertyInfo.PropertyType == typeof(string);
        if (isAirportCodeProperty && isStringProperty)
            return RandomAirportCode();

        return new NoSpecimen();
    }

    private static string RandomAirportCode()
        => DateTime.Now.Ticks % 2 == 0 ? "LHR" : "PER";
}

public class AirportCodeGenerator : ISpecimenBuilder
{
    public object Create(object request, ISpecimenContext context)
        => request switch
        {
            ParameterInfo parameterInfo => parameterInfo.ParameterType == typeof(AirportCode)
                ? RandomAirportCode()
                : new NoSpecimen(),
            PropertyInfo propertyInfo => propertyInfo.PropertyType == typeof(AirportCode)
                ? RandomAirportCode()
                : new NoSpecimen(),
            _ => new NoSpecimen()
        };

    private static AirportCode RandomAirportCode()
        => DateTime.Now.Ticks % 2 == 0 ? (AirportCode)"LHR" : (AirportCode)"PER";
}