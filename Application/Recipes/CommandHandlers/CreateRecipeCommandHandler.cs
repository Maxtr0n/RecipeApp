using Application.Common.Abstractions.CQRS;
using Application.Common.Abstractions.Repositories;
using Application.Common.Dtos;
using Application.Recipes.Commands;
using AutoMapper;
using Domain.Entities;

namespace Application.Recipes.CommandHandlers;

public class CreateRecipeCommandHandler(IMapper mapper, IRepository<Recipe> repository) : ICommandHandler<CreateRecipeCommand, RecipeReadDto>
{
    public async Task<RecipeReadDto> Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
    {
        var recipe = mapper.Map<Recipe>(request.RecipeCreateDto);
        var result = await repository.AddAsync(recipe);

        if (result is null)
        {
            // TODO: throw correct exception/ ardalis result
            throw new Exception();
        }

        await repository.SaveChangesAsync();

        return mapper.Map<RecipeReadDto>(result);
    }
}
