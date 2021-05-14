namespace GameEngine
{
    using System;

    public class NonPlayerCharacter
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => $"{this.FirstName} {this.LastName}";

        public int Health { get; set; } = 100;

        public void TakeDamage(int damage) => this.Health = Math.Max(1, this.Health -= damage);
    }
}
