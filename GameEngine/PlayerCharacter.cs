namespace GameEngine
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using GameEngine.Annotations;

    public class PlayerCharacter : INotifyPropertyChanged
    {
        private int health = 100;

        public PlayerCharacter()
        {
            this.IsNoob = true;
            this.CreateStartingWeapons();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => $"{this.FirstName} {this.LastName}";

        public string Nickname { get; set; }

        public int Health
        {
            get => this.health;
            set
            {
                this.health = value;
                this.OnPropertyChanged();
            }
        }

        public bool IsNoob { get; set; }

        public List<string> Weapons { get; set; }

        public event EventHandler<EventArgs> PlayerSlept; 

        public event PropertyChangedEventHandler PropertyChanged;

        public void Sleep()
        {
            var healthIncrease = this.CalculateHealthIncrease();
            this.Health += healthIncrease;
            this.OnPlayerSlept(EventArgs.Empty);
        }

        public void TakeDamage(int damage) => this.Health = Math.Max(1, this.Health -= damage);

        protected virtual void OnPlayerSlept(EventArgs e) => this.PlayerSlept?.Invoke(this, e);

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private int CalculateHealthIncrease() => new Random().Next(1, 101);

        private void CreateStartingWeapons() => this.Weapons = new List<string>
        {
            "Long Bow",
            "Short Bow",
            "Short Sword"
        };
    }
}
