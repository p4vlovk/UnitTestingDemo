namespace DemoCode.Tests;

using AutoFixture;
using Xunit;

public class NumberDemos
{
    [Fact]
    public void Ints()
    {
        // Arrange
        var sut = new IntCalculator();
        var fixture = new Fixture();
        var number = fixture.Create<int>();

        // Act
        sut.Subtract(number);

        // Assert
        Assert.True(sut.Value < 0);
    }

    [Fact]
    public void Decimals()
    {
        // Arrange
        var sut = new DecimalCalculator();
        var fixture = new Fixture();
        var number = fixture.Create<decimal>();

        // Act
        sut.Add(number);

        // Assert
        Assert.Equal(number, sut.Value);
    }
}