namespace GameEngine;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using GameEngine.Annotations;

public sealed class PlayerCharacter : INotifyPropertyChanged
{
    public const int DefaultHealth = 100;
    
    private readonly string firstName;
    private readonly string lastName;
        
    private int health = DefaultHealth;

    public PlayerCharacter(string firstName, string lastName, string nickname = null)
    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.Nickname = nickname;
        this.IsNoob = true;
        this.CreateStartingWeapons();
    }

    public string FullName => $"{this.firstName} {this.lastName}";

    public string Nickname { get; }
        
    public bool IsNoob { get; }

    public int Health
    {
        get => this.health;
        set
        {
            this.health = value;
            this.OnPropertyChanged();
        }
    }

    public List<string> Weapons { get; private set; }

    public event EventHandler<EventArgs> PlayerSlept;

    public event PropertyChangedEventHandler PropertyChanged;

    public void Sleep()
    {
        var healthIncrease = CalculateHealthIncrease();
        this.Health += healthIncrease;
        this.OnPlayerSlept(EventArgs.Empty);
    }

    public void TakeDamage(int damage) => this.Health = Math.Max(1, this.Health -= damage);

    private void OnPlayerSlept(EventArgs e) => this.PlayerSlept?.Invoke(this, e);

    [NotifyPropertyChangedInvocator]
    private void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private static int CalculateHealthIncrease() => new Random().Next(1, 101);

    private void CreateStartingWeapons() => this.Weapons = new List<string>
    {
        "Long Bow",
        "Short Bow",
        "Short Sword"
    };
}