namespace GameEngine.Tests
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class ExternalHealthDamageTestData
    {
        public static IEnumerable<object[]> TestData =>
            File.ReadAllLines("TestData.csv")
                .Select(csvLine => csvLine
                    .Split(',')
                    .Select(int.Parse))
                .Select(values => values
                    .Cast<object>()
                    .ToArray());
    }
}
