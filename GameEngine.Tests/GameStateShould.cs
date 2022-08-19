namespace GameEngine.Tests;

using Xunit;
using Xunit.Abstractions;

public class GameStateShould : IClassFixture<GameStateFixture>
{
    private readonly GameStateFixture gameStateFixture;
    private readonly ITestOutputHelper output;

    public GameStateShould(GameStateFixture gameStateFixture, ITestOutputHelper output)
    {
        this.gameStateFixture = gameStateFixture;
        this.output = output;
    }

    [Fact]
    public void DamageAllPlayersWhenEarthquake()
    {
        this.output.WriteLine($"GameState ID={this.gameStateFixture.State.Id}");
        var player1 = new PlayerCharacter("Sarah", "Smith");
        var player2 = new PlayerCharacter("John", "Doe");
        this.gameStateFixture.State.Players.Add(player1);
        this.gameStateFixture.State.Players.Add(player2);
        var expectedHealthAfterEarthquake = player1.Health - GameState.EarthquakeDamage;
            
        this.gameStateFixture.State.Earthquake();
            
        Assert.Equal(expectedHealthAfterEarthquake, player1.Health);
        Assert.Equal(expectedHealthAfterEarthquake, player2.Health);
    }

    [Fact]
    public void Reset()
    {
        this.output.WriteLine($"GameState ID={this.gameStateFixture.State.Id}");
        var player1 = new PlayerCharacter("Sarah", "Smith");
        var player2 = new PlayerCharacter("John", "Doe");
        this.gameStateFixture.State.Players.Add(player1);
        this.gameStateFixture.State.Players.Add(player2);
            
        this.gameStateFixture.State.Reset();
            
        Assert.Empty(this.gameStateFixture.State.Players);
    }
}