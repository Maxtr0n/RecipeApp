using FluentValidation;

namespace Application.Recipes.GetById;

public class GetRecipeByIdQueryValidator : AbstractValidator<GetRecipeByIdQuery>
{
    public GetRecipeByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}
