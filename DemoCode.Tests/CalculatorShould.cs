namespace DemoCode.Tests
{
    using AutoFixture.Xunit2;
    using Xunit;

    public class CalculatorShould
    {
        [Theory]
        [AutoData]
        public void AddTwoPositiveNumbers(int a, int b, Calculator sut)
        {
            sut.Add(a);
            sut.Add(b);

            Assert.Equal(a + b, sut.Value);
        }

        // By default, AutoFixture generates positive numbers
        [Theory]
        [InlineAutoData] // Add two positive numbers
        [InlineAutoData(0)] // Add zero and a positive number
        [InlineAutoData(-5)] // Add a negative and a positive number
        public void Add(int a, int b, Calculator sut)
        {
            sut.Add(a);
            sut.Add(b);

            Assert.Equal(a + b, sut.Value);
        }
    }
}
