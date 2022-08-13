namespace GameEngine
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public class GameState
    {
        public const int EarthquakeDamage = 25;

        public GameState() => CreateGameWorld();

        public List<PlayerCharacter> Players { get; } = new();

        public Guid Id { get; } = Guid.NewGuid();

        public void Earthquake()
        {
            foreach (var player in this.Players)
            {
                player.TakeDamage(EarthquakeDamage);
            }
        }

        public void Reset() => this.Players.Clear();

        private static void CreateGameWorld() => Thread.Sleep(2000); // Simulate expensive creation
    }
}
