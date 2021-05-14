namespace GameEngine.Tests
{
    using System;

    public class GameStateFixture : IDisposable
    {
        public GameStateFixture()
        {
            this.State = new GameState();
        }

        public GameState State { get; private set; }

        public void Dispose()
        {
            // Cleanup
        }
    }
}
