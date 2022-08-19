namespace GameEngine.Tests;

using Xunit;
using Xunit.Abstractions;

[Collection("GameState collection")]
public class TestClass2 : IClassFixture<GameStateFixture>
{
    private readonly GameStateFixture gameStateFixture;
    private readonly ITestOutputHelper output;

    public TestClass2(GameStateFixture gameStateFixture, ITestOutputHelper output)
    {
        this.gameStateFixture = gameStateFixture;
        this.output = output;
    }

    [Fact]
    public void Test3() => this.output.WriteLine($"GameState ID={this.gameStateFixture.State.Id}");

    [Fact]
    public void Test4() => this.output.WriteLine($"GameState ID={this.gameStateFixture.State.Id}");
}