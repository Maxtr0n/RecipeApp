using Application.Common.Dtos;
using Application.Recipes.Create;
using Application.Recipes.Delete;
using Application.Recipes.GetAll;
using Application.Recipes.GetById;
using Application.Recipes.Update;
using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace RecipeApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController(IMediator mediator, UserManager<ApplicationUser> userManager) : ControllerBase
{
    [HttpGet("{id}")]
    [TranslateResultToActionResult]
    public async Task<Result<RecipeReadDto>> GetRecipeById([FromRoute] Guid id)
    {
        return await mediator.Send(new GetRecipeByIdQuery(id));
    }

    [HttpGet]
    [TranslateResultToActionResult]
    public async Task<Result<List<RecipeReadDto>>> GetRecipes()
    {
        return await mediator.Send(new GetAllRecipesQuery());
    }

    [HttpPost]
    [Authorize]
    [TranslateResultToActionResult]
    public async Task<Result<RecipeReadDto>> CreateRecipe([FromBody] RecipeCreateDto dto)
    {
        var userName = User.Identity?.Name;

        if (string.IsNullOrEmpty(userName))
        {
            return Result<RecipeReadDto>.Unauthorized();
        }

        var user = await userManager.FindByNameAsync(userName);

        if (user == null)
        {
            return Result<RecipeReadDto>.Unauthorized();
        }

        return await mediator.Send(new CreateRecipeCommand(dto, user));
    }

    [HttpDelete("{id}")]
    [Authorize]
    [TranslateResultToActionResult]
    public async Task<Result> DeleteRecipe([FromRoute] Guid id)
    {
        return await mediator.Send(new DeleteRecipeCommand(id));
    }

    [HttpPut("{id}")]
    [Authorize]
    [TranslateResultToActionResult]
    public async Task<Result<RecipeReadDto>> UpdateRecipe([FromRoute] Guid id,
        [FromBody] RecipeUpdateDto recipeUpdateDto)
    {
        return await mediator.Send(new UpdateRecipeCommand(id, recipeUpdateDto));
    }
}