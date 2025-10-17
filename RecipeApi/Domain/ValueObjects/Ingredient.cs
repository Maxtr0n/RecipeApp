using Ardalis.GuardClauses;
using SharedKernel;

namespace Domain.ValueObjects;

public class Ingredient : ValueObject
{
    protected Ingredient()
    {
    }

    public Ingredient(string name, Quantity quantity)
    {
        Guard.Against.NullOrWhiteSpace(name);

        Name = name;
        Quantity = quantity;
    }

    public string Name { get; private set; }

    public Quantity Quantity { get; private set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Quantity;
    }
}