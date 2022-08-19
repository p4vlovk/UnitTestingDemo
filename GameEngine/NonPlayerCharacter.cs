namespace GameEngine;

using System;

public class NonPlayerCharacter
{
    private readonly string firstName;
    private readonly string lastName;

    public NonPlayerCharacter(string firstName, string lastName)
    {
        this.firstName = firstName;
        this.lastName = lastName;
    }
        
    public string FullName => $"{this.firstName} {this.lastName}";

    public int Health { get; private set; } = 100;

    public void TakeDamage(int damage) => this.Health = Math.Max(1, this.Health -= damage);
}