namespace ICMON.Domain.ValueObjects;

public class Money : ValueObject
{
    public decimal Amount { get; }
    public string Currency { get; }

    public static Money Zero => new(0, "THB");

    public Money(decimal amount, string currency = "THB")
    {
        if (amount < 0) throw new DomainException("Amount cannot be negative");
        Amount = amount;
        Currency = currency;
    }

    public Money Add(Money other)
    {
        if (Currency != other.Currency)
            throw new DomainException("Currency mismatch");
        return new Money(Amount + other.Amount, Currency);
    }

    public static Money operator +(Money a, Money b) => a.Add(b);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }

    public override string ToString() => $"{Amount:N2} {Currency}";
}
