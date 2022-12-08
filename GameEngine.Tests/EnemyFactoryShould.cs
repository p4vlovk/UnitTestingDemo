namespace GameEngine.Tests;

using System;
using Xunit;

[Trait("Category", "Enemy")]
public sealed class EnemyFactoryShould
{
    [Fact]
    public void CreateNormalEnemyByDefault()
    {
        var enemy = EnemyFactory.Create("Zombie");

        Assert.IsType<NormalEnemy>(enemy);
    }

    [Fact]
    public void CreateBossEnemy()
    {
        var enemy = EnemyFactory.Create("Zombie King", true);

        Assert.IsType<BossEnemy>(enemy);
    }

    [Fact]
    public void CreateBossEnemy_CastReturnedTypeExample()
    {
        var enemy = EnemyFactory.Create("Zombie King", true);

        var boss = Assert.IsType<BossEnemy>(enemy); // Assert and get cast result
        Assert.Equal("Zombie King", boss.Name); // Additional assert on typed object
    }

    [Fact]
    public void CreateBossEnemy_AssertAssignableTypes()
    {
        var enemy = EnemyFactory.Create("Zombie King", true);

        //Assert.IsType<Enemy>(enemy);
        Assert.IsAssignableFrom<Enemy>(enemy);
    }

    [Fact]
    public void CreateSeparateInstances()
    {
        var enemy1 = EnemyFactory.Create("Zombie");
        var enemy2 = EnemyFactory.Create("Zombie");
            
        Assert.NotSame(enemy1, enemy2);
    }

    [Fact]
    public void NotAllowNullName()
    {
        //Assert.Throws<ArgumentNullException>(() => EnemyFactory.Create(null));
        Assert.Throws<ArgumentNullException>("name", () => EnemyFactory.Create(null));
    }

    [Fact]
    public void OnlyAllowKingOrQueenBossEnemies()
    {
        var ex = Assert.Throws<EnemyCreationException>(() => EnemyFactory.Create("Zombie", true));
        Assert.Equal("Zombie", ex.RequestedEnemyName);
    }
}