﻿namespace GameEngine;

public abstract class Enemy
{
    public string Name { get; init; }

    public abstract double TotalSpecialPower { get; }

    public abstract double SpecialPowerUses { get; }

    public double SpecialAttackPower => this.TotalSpecialPower / this.SpecialPowerUses;
}