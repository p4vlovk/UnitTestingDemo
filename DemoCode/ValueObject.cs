namespace DemoCode;

using System.Collections.Generic;
using System.Linq;

public abstract class ValueObject
{
    protected abstract IEnumerable<object> GetEqualityComponents();

    public override bool Equals(object obj)
        => obj is not null
           && this.GetType() == obj.GetType()
           && this.GetEqualityComponents()
               .SequenceEqual(((ValueObject)obj).GetEqualityComponents());

    public override int GetHashCode()
        => this.GetEqualityComponents()
            .Aggregate(1, (current, obj) =>
            {
                unchecked
                {
                    return current * 23 + (obj?.GetHashCode() ?? 0);
                }
            });

    public static bool operator ==(ValueObject objA, ValueObject objB)
        => objA?.Equals(objB) ?? objB is null;

    public static bool operator !=(ValueObject objA, ValueObject objB)
        => !(objA == objB);
}