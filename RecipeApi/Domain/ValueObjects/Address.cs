using SharedKernel;

namespace Domain.ValueObjects;

public class Address(string street, string city, string state, string zipCode, string country)
    : ValueObject
{
    public string Street { get; init; } = street;
    public string City { get; init; } = city;
    public string State { get; init; } = state;
    public string ZipCode { get; init; } = zipCode;
    public string Country { get; init; } = country;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Street;
        yield return City;
        yield return State;
        yield return ZipCode;
        yield return Country;
    }
}