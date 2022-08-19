namespace UnitTestingDemo.Api.Models;

using System.Collections.Generic;
using System.Linq;

public class Item
{
    private readonly int id;
    private readonly List<Tag> tags = new List<Tag>();

    private Item(int id, string name)
    {
        this.id = id;
        this.Name = name;
    }

    public Item(string name)
    {
        this.Name = name;
    }

    public string Name { get; }

    public IReadOnlyList<Tag> Tags => this.tags;

    public Tag AddTag(string label)
    {
        var tag = this.tags.FirstOrDefault(t => t.Label == label);
        if (tag == null)
        {
            tag = new Tag(label);
            this.tags.Add(tag);
        }

        tag.Count++;

        return tag;
    }
}