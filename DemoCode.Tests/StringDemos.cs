namespace DemoCode.Tests;

using AutoFixture;
using Xunit;

public class StringDemos
{
    [Fact]
    public void BasicStrings()
    {
        var sut = new NameJoiner();
        var fixture = new Fixture();
        var firstName = fixture.Create("First_"); // AutoFixture.SeedExtensions NuGet package
        var lastName = fixture.Create("Last_");

        var result = sut.Join(firstName, lastName);

        Assert.Equal($"{firstName} {lastName}", result);
    }
}