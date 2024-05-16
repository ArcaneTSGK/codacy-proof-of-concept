using Ardalis.GuardClauses;

namespace Codacy.Proof.SecondMonolithicModule.Domain.Games;

internal class GameName
{
    public string Value { get; }

    public GameName(string value)
    {
        Value = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }
}
