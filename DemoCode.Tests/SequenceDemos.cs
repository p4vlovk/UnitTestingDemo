namespace DemoCode.Tests;

using System;
using AutoFixture;
using Xunit;

public class SequenceDemos
{
    [Fact]
    public void SequenceOfStrings()
    {
        var fixture = new Fixture();
        var messages = fixture.CreateMany<string>(); // The sequence has 3 elements by default

        // etc. 
    }

    [Fact]
    public void ExplicitNumberOfItems()
    {
        var fixture = new Fixture();
        var numbers = fixture.CreateMany<int>(6);

        // etc.
    }

    [Fact]
    public void AddingToExistingList()
    {
        var fixture = new Fixture();
        var sut = new DebugMessageBuffer();
        fixture.AddManyTo(sut.Messages, 10);

        sut.WriteMessage();

        Assert.Equal(10, sut.MessagesWritten);
    }

    [Fact]
    public void AddingToExistingListWithCreatorFunction()
    {
        var fixture = new Fixture();
        var sut = new DebugMessageBuffer();
        // fixture.AddManyTo(sut.Messages, () => "hi");
        var random = new Random();
        fixture.AddManyTo(sut.Messages, () => random.Next().ToString());
            
        // etc.
    }
}