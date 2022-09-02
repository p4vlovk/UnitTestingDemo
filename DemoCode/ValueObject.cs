namespace DemoCode;

using System.Collections.Generic;
using System.Linq;

public abstract class ValueObject
{
    protected abstract IEnumerable<object> GetEqualityComponents();

    public override bool Equals(object obj)
        => ReferenceEquals(this, obj) ||
           (obj is ValueObject other &&
            this.GetType() == other.GetType() &&
            this.GetEqualityComponents().SequenceEqual(other.GetEqualityComponents()));

    public override int GetHashCode()
        => this.GetEqualityComponents()
            .Where(value => value is not null)
            .Aggregate(17, (current, value) =>
            {
                unchecked
                {
                    return current * 59 + value.GetHashCode();
                }
            });

    public static bool operator ==(ValueObject left, ValueObject right)
        => left?.Equals(right) ?? right is null;

    public static bool operator !=(ValueObject left, ValueObject right)
        => !(left == right);
}