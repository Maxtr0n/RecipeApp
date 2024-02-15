using Application.Common.Dtos;
using Application.Recipes.Commands;
using Application.Recipes.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace RecipeApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController(IMediator mediator) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<RecipeReadDto>> GetRecipeById([FromRoute] Guid id)
    {
        var result = await mediator.Send(new GetRecipeByIdQuery(id));
        return Ok(result.Value);
    }

    [HttpGet]
    public async Task<ActionResult<List<RecipeReadDto>>> GetRecipes()
    {
        var result = await mediator.Send(new GetAllRecipesQuery());
        return Ok(result.Value);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RecipeReadDto>> CreateRecipe([FromBody] RecipeCreateDto dto)
    {
        var result = await mediator.Send(new CreateRecipeCommand(dto));
        //TODO: return CreatedAtAction result or something like that
        return Created();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteRecipe([FromRoute] Guid id)
    {
        var result = await mediator.Send(new DeleteRecipeCommand(id));
        //TODO what to return after delete
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<RecipeReadDto>> UpdateRecipe([FromRoute] Guid id, [FromBody] RecipeUpdateDto recipeUpdateDto)
    {
        var result = await mediator.Send(new UpdateRecipeCommand(id, recipeUpdateDto));

        return Ok(result.Value);
    }

}
