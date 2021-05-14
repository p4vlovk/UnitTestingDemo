namespace GameEngine.Tests
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Xunit.Sdk;

    public class HealthDamageDataAttribute : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            // Read hard-coded data
            //yield return new object[] { 0, 100 };
            //yield return new object[] { 1, 99 };
            //yield return new object[] { 50, 50 };
            //yield return new object[] { 101, 1 };

            // Read data from an external source
            string[] csvLines = File.ReadAllLines("TestData.csv");
            List<object[]> testCases = new List<object[]>();
            testCases.AddRange(csvLines
                .Select(csvLine => csvLine.Split(',').Select(int.Parse))
                .Select(values => values.Cast<object>().ToArray()));

            return testCases;
        }
    }
}
