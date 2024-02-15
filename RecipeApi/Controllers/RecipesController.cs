using Application.Common.Dtos;
using Application.Recipes.Commands;
using Application.Recipes.Queries;
using Ardalis.Result.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace RecipeApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController(IMediator mediator) : ControllerBase
{
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RecipeReadDto>> GetRecipeById([FromRoute] Guid id)
    {
        return this.ToActionResult(await mediator.Send(new GetRecipeByIdQuery(id)));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<RecipeReadDto>>> GetRecipes()
    {
        return this.ToActionResult(await mediator.Send(new GetAllRecipesQuery()));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RecipeReadDto>> CreateRecipe([FromBody] RecipeCreateDto dto)
    {
        //TODO: return CreatedAtAction result or something like that
        return this.ToActionResult(await mediator.Send(new CreateRecipeCommand(dto)));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteRecipe([FromRoute] Guid id)
    {
        return this.ToActionResult(await mediator.Send(new DeleteRecipeCommand(id)));
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RecipeReadDto>> UpdateRecipe([FromRoute] Guid id, [FromBody] RecipeUpdateDto recipeUpdateDto)
    {
        return this.ToActionResult(await mediator.Send(new UpdateRecipeCommand(id, recipeUpdateDto)));
    }

}
