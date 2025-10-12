using FluentValidation;

namespace Application.Recipes.Create;

public class CreateRecipeCommandValidator : AbstractValidator<CreateRecipeCommand>
{
    public CreateRecipeCommandValidator()
    {
        RuleFor(x => x.RecipeCreateDto)
            .NotNull();

        RuleFor(x => x.RecipeCreateDto.Title)
            .NotEmpty()
            .WithMessage("Title is required.")
            .MinimumLength(3)
            .MaximumLength(100);

        RuleFor(x => x.RecipeCreateDto.Instructions)
            .NotEmpty()
            .WithMessage("Description is required.")
            .MinimumLength(3)
            .MaximumLength(5000);

        RuleFor(x => x.RecipeCreateDto.Ingredients).NotEmpty();
    }
}