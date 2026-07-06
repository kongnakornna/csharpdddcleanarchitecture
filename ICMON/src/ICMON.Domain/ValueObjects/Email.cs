using System.Text.RegularExpressions;

namespace ICMON.Domain.ValueObjects;

public partial class Email : ValueObject
{
    private static readonly Regex EmailRegex = EmailValidationRegex();

    public string Value { get; }

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("Email cannot be empty");

        if (!EmailRegex.IsMatch(value))
            throw new DomainException($"Invalid email format: {value}");

        Value = value.ToLowerInvariant().Trim();
    }

    [GeneratedRegex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")]
    private static partial Regex EmailValidationRegex();

    public override string ToString() => Value;

    public static implicit operator string(Email email) => email.Value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
