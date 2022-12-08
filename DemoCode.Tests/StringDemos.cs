namespace DemoCode.Tests;

using AutoFixture;
using Xunit;

public class StringDemos
{
    [Fact]
    public void BasicStrings()
    {
        // Arrange
        var fixture = new Fixture();
        var firstName = fixture.Create("First_"); // AutoFixture.SeedExtensions NuGet package
        var lastName = fixture.Create("Last_");

        // Act
        var result = NameJoiner.Join(firstName, lastName);

        // Assert
        Assert.Equal($"{firstName} {lastName}", result);
    }
}