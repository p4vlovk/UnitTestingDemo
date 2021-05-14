namespace DemoCode.Tests
{
    using AutoFixture;
    using Xunit;

    public class NumberDemos
    {
        [Fact]
        public void Ints()
        {
            var sut = new IntCalculator();
            var fixture = new Fixture();

            sut.Subtract(fixture.Create<int>());

            Assert.True(sut.Value < 0);
        }

        [Fact]
        public void Decimals()
        {
            var sut = new DecimalCalculator();
            var fixture = new Fixture();
            var num = fixture.Create<decimal>();

            sut.Add(num);

            Assert.Equal(num, sut.Value);
        }
    }
}
