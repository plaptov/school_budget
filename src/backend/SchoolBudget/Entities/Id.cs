using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SchoolBudget.Entities;

public readonly struct Id<T>
{
    private readonly long _value;

    public Id(long value)
    {
        this._value = value;
    }

    public override int GetHashCode()
    {
        return _value.GetHashCode();
    }

    public override string? ToString()
    {
        return _value.ToString();
    }

    public class EfCoreConverter : ValueConverter<Id<T>, long>
    {
        public EfCoreConverter()
            : base(
                v => v._value,
                l => new Id<T>(l)
            )
        {

        }
    }
}
