namespace GameEngine
{
    public sealed class BossEnemy : Enemy
    {
        public override double TotalSpecialPower => 1000;

        public override double SpecialPowerUses => 6;
    }
}
