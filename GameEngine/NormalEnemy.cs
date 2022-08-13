namespace GameEngine
{
    public sealed class NormalEnemy : Enemy
    {
        public override double TotalSpecialPower => 100;

        public override double SpecialPowerUses => 2;
    }
}
