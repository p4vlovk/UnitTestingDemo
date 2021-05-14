namespace DemoCode
{
    public class DecimalCalculator
    {
        public decimal Value { get; private set; }

        public void Add(decimal number) => this.Value += number;

        public void Subtract(decimal number) => this.Value -= number;
    }
}
