namespace GameEngine.Tests
{
    using System.Collections.Generic;

    public class InternalHealthDamageTestData
    {
        public static IEnumerable<object[]> TestData => new List<object[]>
        {
            new object[] { 0, 100 },
            new object[] { 1, 99 },
            new object[] { 50, 50 },
            new object[] { 101, 1 }
        };
    }
}
