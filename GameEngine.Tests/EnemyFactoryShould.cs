namespace GameEngine.Tests
{
    using System;
    using Xunit;

    [Trait("Category", "Enemy")]
    public class EnemyFactoryShould
    {
        private readonly EnemyFactory sut;

        public EnemyFactoryShould()
        {
            this.sut = new EnemyFactory();
        }

        [Fact]
        public void CreateNormalEnemyByDefault()
        {
            var enemy = sut.Create("Zombie");

            Assert.IsType<NormalEnemy>(enemy);
        }

        [Fact]
        public void CreateBossEnemy()
        {
            var enemy = sut.Create("Zombie King", true);

            Assert.IsType<BossEnemy>(enemy);
        }

        [Fact]
        public void CreateBossEnemy_CastReturnedTypeExample()
        {
            var enemy = sut.Create("Zombie King", true);

            var boss = Assert.IsType<BossEnemy>(enemy); // Assert and get cast result
            Assert.Equal("Zombie King", boss.Name); // Additional assert on typed object
        }

        [Fact]
        public void CreateBossEnemy_AssertAssignableTypes()
        {
            var enemy = sut.Create("Zombie King", true);

            //Assert.IsType<Enemy>(enemy);
            Assert.IsAssignableFrom<Enemy>(enemy);
        }

        [Fact]
        public void CreateSeparateInstances()
        {
            var enemy1 = sut.Create("Zombie");
            var enemy2 = sut.Create("Zombie");
            
            Assert.NotSame(enemy1, enemy2);
        }

        [Fact]
        public void NotAllowNullName()
        {
            //Assert.Throws<ArgumentNullException>(() => sut.Create(null));
            Assert.Throws<ArgumentNullException>("name", () => sut.Create(null));
        }

        [Fact]
        public void OnlyAllowKingOrQueenBossEnemies()
        {
            var ex = Assert.Throws<EnemyCreationException>(() => sut.Create("Zombie", true));
            Assert.Equal("Zombie", ex.RequestedEnemyName);
        }
    }
}
