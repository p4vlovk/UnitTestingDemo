namespace GameEngine.Tests
{
    using System;
    using Xunit;

    [Trait("Category", "Enemy")]
    public class EnemyFactoryShould
    {
        [Fact]
        public void CreateNormalEnemyByDefault()
        {
            // Arrange
            var sut = new EnemyFactory();

            // Act
            var enemy = sut.Create("Zombie");

            // Assert
            Assert.IsType<NormalEnemy>(enemy);
        }

        [Fact]
        public void CreateBossEnemy()
        {
            // Arrange
            var sut = new EnemyFactory();

            // Act
            var enemy = sut.Create("Zombie King", true);

            // Assert
            Assert.IsType<BossEnemy>(enemy);
        }

        [Fact]
        public void CreateBossEnemy_CastReturnedTypeExample()
        {
            // Arrange
            var sut = new EnemyFactory();

            // Act
            var enemy = sut.Create("Zombie King", true);

            // Assert
            var boss = Assert.IsType<BossEnemy>(enemy); // Assert and get cast result
            Assert.Equal("Zombie King", boss.Name); // Additional assert on typed object
        }

        [Fact]
        public void CreateBossEnemy_AssertAssignableTypes()
        {
            // Arrange
            var sut = new EnemyFactory();

            // Act
            var enemy = sut.Create("Zombie King", true);

            // Assert
            //Assert.IsType<Enemy>(enemy);
            Assert.IsAssignableFrom<Enemy>(enemy);
        }

        [Fact]
        public void CreateSeparateInstances()
        {
            // Arrange
            var sut = new EnemyFactory();

            // Act
            var enemy1 = sut.Create("Zombie");
            var enemy2 = sut.Create("Zombie");

            // Assert
            Assert.NotSame(enemy1, enemy2);
        }

        [Fact]
        public void NotAllowNullName()
        {
            // Arrange
            var sut = new EnemyFactory();

            // Assert
            //Assert.Throws<ArgumentNullException>(() => sut.Create(null));
            Assert.Throws<ArgumentNullException>("name", () => sut.Create(null));
        }

        [Fact]
        public void OnlyAllowKingOrQueenBossEnemies()
        {
            // Arrange
            var sut = new EnemyFactory();

            // Assert
            var ex = Assert.Throws<EnemyCreationException>(() => sut.Create("Zombie", true));
            Assert.Equal("Zombie", ex.RequestedEnemyName);
        }
    }
}
