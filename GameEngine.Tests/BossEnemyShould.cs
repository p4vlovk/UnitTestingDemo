namespace GameEngine.Tests
{
    using Xunit;
    using Xunit.Abstractions;

    public class BossEnemyShould
    {
        private readonly ITestOutputHelper output;

        public BossEnemyShould(ITestOutputHelper output) => this.output = output;

        [Fact]
        [Trait("Category", "Enemy")]
        public void HaveCorrectPower()
        {
            this.output.WriteLine("Creating Boss Enemy");
            var sut = new BossEnemy();
            
            Assert.Equal(166.667, sut.SpecialAttackPower, 3);
        }
    }
}
