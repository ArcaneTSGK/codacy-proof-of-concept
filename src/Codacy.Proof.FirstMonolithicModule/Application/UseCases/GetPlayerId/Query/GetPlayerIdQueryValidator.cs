using FluentValidation;

namespace Codacy.Proof.FirstMonolithicModule.Application.UseCases.GetPlayerId.Query;

internal class GetPlayerIdQueryValidator : AbstractValidator<GetPlayerIdQuery>
{
    public GetPlayerIdQueryValidator()
    {
        RuleFor(x => x.Login)
            .NotEmpty();
    }
}
