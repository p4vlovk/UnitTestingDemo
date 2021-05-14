namespace GameEngine
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public class GameState
    {
        public static readonly int EarthquakeDamage = 25;

        public GameState()
        {
            this.CreateGameWorld();
        }

        public List<PlayerCharacter> Players { get; set; } = new List<PlayerCharacter>();

        public Guid Id { get; } = Guid.NewGuid();

        public void Earthquake()
        {
            foreach (var player in this.Players)
            {
                player.TakeDamage(EarthquakeDamage);
            }
        }

        public void Reset() => this.Players.Clear();

        private void CreateGameWorld() => Thread.Sleep(2000); // Simulate expensive creation
    }
}
