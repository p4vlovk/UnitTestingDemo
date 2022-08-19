namespace DemoCode;

public class IntCalculator
{
    public int Value { get; private set; }

    public void Add(int number) => this.Value += number;

    public void Subtract(int number) => this.Value -= number;
}