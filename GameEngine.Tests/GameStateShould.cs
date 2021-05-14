namespace GameEngine.Tests
{
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
            // Arrange
            this.output.WriteLine($"GameState ID={this.gameStateFixture.State.Id}");
            var player1 = new PlayerCharacter();
            var player2 = new PlayerCharacter();
            this.gameStateFixture.State.Players.Add(player1);
            this.gameStateFixture.State.Players.Add(player2);
            var expectedHealthAfterEarthquake = player1.Health - GameState.EarthquakeDamage;

            // Act
            this.gameStateFixture.State.Earthquake();

            // Assert
            Assert.Equal(expectedHealthAfterEarthquake, player1.Health);
            Assert.Equal(expectedHealthAfterEarthquake, player2.Health);
        }

        [Fact]
        public void Reset()
        {
            // Arrange
            this.output.WriteLine($"GameState ID={this.gameStateFixture.State.Id}");
            var player1 = new PlayerCharacter();
            var player2 = new PlayerCharacter();
            this.gameStateFixture.State.Players.Add(player1);
            this.gameStateFixture.State.Players.Add(player2);

            // Act
            this.gameStateFixture.State.Reset();

            // Assert
            Assert.Empty(this.gameStateFixture.State.Players);
        }
    }
}
