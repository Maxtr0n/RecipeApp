using Ardalis.GuardClauses;
using SharedKernel;

namespace Domain.ValueObjects;

public class Quantity : ValueObject
{
    protected Quantity()
    {
    }

    public Quantity(double amount, string unit)
    {
        Guard.Against.NegativeOrZero(amount);
        Guard.Against.NullOrWhiteSpace(unit);

        Amount = amount;
        Unit = unit;
    }

    public double Amount { get; }

    public string Unit { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Unit;
    }
}