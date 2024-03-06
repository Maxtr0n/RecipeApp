using FluentValidation;

namespace Application.Recipes.Delete;

public class DeleteRecipeCommandValidator : AbstractValidator<DeleteRecipeCommand>
{
    public DeleteRecipeCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}
