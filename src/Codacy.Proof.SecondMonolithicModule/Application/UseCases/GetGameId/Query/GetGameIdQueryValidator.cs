using FluentValidation;

namespace Codacy.Proof.SecondMonolithicModule.Application.UseCases.GetGameId.Query;

internal class GetGameIdQueryValidator : AbstractValidator<GetGameIdQuery>
{
    public GetGameIdQueryValidator()
    {
        RuleFor(x => x.GameName)
            .NotEmpty();
    }
}
