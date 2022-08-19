namespace UnitTestingDemo.Api.Models;

public class Tag
{
    private readonly int id;

    private Tag(int id, string label)
    {
        this.id = id;
        this.Label = label;
    }

    public Tag(string label)
    {
        this.Label = label;
    }

    public string Label { get; }

    public int Count { get; set; }
}