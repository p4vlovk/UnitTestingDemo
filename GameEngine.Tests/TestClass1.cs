namespace GameEngine.Tests
{
    using Xunit;
    using Xunit.Abstractions;

    [Collection("GameState collection")]
    public class TestClass1 : IClassFixture<GameStateFixture>
    {
        private readonly GameStateFixture gameStateFixture;
        private readonly ITestOutputHelper output;

        public TestClass1(GameStateFixture gameStateFixture, ITestOutputHelper output)
        {
            this.gameStateFixture = gameStateFixture;
            this.output = output;
        }

        [Fact]
        public void Test1() => this.output.WriteLine($"GameState ID={this.gameStateFixture.State.Id}");

        [Fact]
        public void Test2() => this.output.WriteLine($"GameState ID={this.gameStateFixture.State.Id}");
    }
}
