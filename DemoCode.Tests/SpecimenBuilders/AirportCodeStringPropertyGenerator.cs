namespace DemoCode.Tests.SpecimenBuilders
{
    using System;
    using System.Reflection;
    using AutoFixture.Kernel;

    /* As an alternative, we can create a custom AirportCode class to represent an airport code.
        We can then use this class here to check the type of the property rather than having to
        use reflection to check the property name. */
    public class AirportCodeStringPropertyGenerator : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            // See if we are trying to create a value for a property
            if (!(request is PropertyInfo propertyInfo))
            {
                // This specimen does not apply to current request
                return new NoSpecimen(); // null is a valid specimen so return NoSpecimen
            }

            // Now we know we are dealing with a property
            // Are we creating a value for an airport code?
            var isAirportCodeProperty = propertyInfo.Name.Contains("AirportCode");
            var isStringProperty = propertyInfo.PropertyType == typeof(string);
            if (isAirportCodeProperty && isStringProperty)
            {
                return this.RandomAirportCode();
            }

            return new NoSpecimen();
        }

        private string RandomAirportCode()
        {
            if (DateTime.Now.Ticks % 2 == 0)
            {
                return "LHR";
            }

            return "PER";
        }
    }
}
