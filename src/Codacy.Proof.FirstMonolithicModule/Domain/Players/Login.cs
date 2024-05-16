using Ardalis.GuardClauses;

namespace Codacy.Proof.FirstMonolithicModule.Domain.Players;

internal class Login
{
    public string Value { get; }

    public Login(string value)
    {
        Value = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }
}
