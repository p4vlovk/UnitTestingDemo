namespace GameEngine.Tests
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class ExternalHealthDamageTestData
    {
        public static IEnumerable<object[]> TestData
        {
            get
            {
                string[] csvLines = File.ReadAllLines("TestData.csv");
                List<object[]> testCases = new List<object[]>();
                testCases.AddRange(csvLines
                    .Select(csvLine => csvLine.Split(',').Select(int.Parse))
                    .Select(values => values.Cast<object>().ToArray()));

                return testCases;
            }
        }
    }
}
