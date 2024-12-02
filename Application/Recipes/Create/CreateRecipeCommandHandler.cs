using Application.Common.Abstractions.CQRS;
using Application.Common.Dtos;
using Application.Common.Extensions;
using Application.Common.Mappings;
using Ardalis.Result;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.Recipes.Create;

public class CreateRecipeCommandHandler(
    IGenericRepository<ApplicationUser> userRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateRecipeCommand, Result<RecipeReadDto>>
{
    public async Task<Result<RecipeReadDto>> Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.User.Id);

        if (user == null)
        {
            return Result<RecipeReadDto>.NotFound($"User with id {request.User.Id} does not exist");
        }

        var recipe = user.AddRecipe(request.RecipeCreateDto.Title,
            request.RecipeCreateDto.Ingredients.JoinStrings(),
            request.RecipeCreateDto.Description, request.RecipeCreateDto.Images.JoinStrings());

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return recipe.MapToReadDto();
    }
}