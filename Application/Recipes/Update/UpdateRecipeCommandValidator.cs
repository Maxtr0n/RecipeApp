using FluentValidation;

namespace Application.Recipes.Update;

public class UpdateRecipeCommandValidator : AbstractValidator<UpdateRecipeCommand>
{
    public UpdateRecipeCommandValidator()
    {
        RuleFor(x => x.RecipeUpdateDto)
           .NotNull();

        RuleFor(x => x.RecipeUpdateDto.Title)
            .NotEmpty()
            .WithMessage("Title is required.")
            .MinimumLength(3)
            .MaximumLength(100);

        RuleFor(x => x.RecipeUpdateDto.Author)
            .NotEmpty()
            .WithMessage("Author is required.")
            .MinimumLength(3)
            .MaximumLength(100);

        RuleFor(x => x.RecipeUpdateDto.Description)
            .NotEmpty()
            .WithMessage("Description is required.")
            .MinimumLength(3)
            .MaximumLength(5000);

        RuleFor(x => x.RecipeUpdateDto.Ingredients).NotEmpty();
    }
}
