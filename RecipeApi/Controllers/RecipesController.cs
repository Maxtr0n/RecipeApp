using Application.Common.Dtos;
using Application.Recipes.Create;
using Application.Recipes.Delete;
using Application.Recipes.GetAll;
using Application.Recipes.GetById;
using Application.Recipes.Update;
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
        var result = await mediator.Send(new GetRecipeByIdQuery(id));

        return result.Value;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<RecipeReadDto>>> GetRecipes()
    {
        var result = await mediator.Send(new GetAllRecipesQuery());

        return result.Value;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RecipeReadDto>> CreateRecipe([FromBody] RecipeCreateDto dto)
    {
        var result = await mediator.Send(new CreateRecipeCommand(dto));

        if (!result.IsSuccess)
        {
            return BadRequest(result.ValidationErrors);
        }

        return CreatedAtAction(nameof(CreateRecipe), new { id = result.Value.Id }, result.Value);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteRecipe([FromRoute] Guid id)
    {
        var result = await mediator.Send(new DeleteRecipeCommand(id));

        return Ok();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RecipeReadDto>> UpdateRecipe([FromRoute] Guid id, [FromBody] RecipeUpdateDto recipeUpdateDto)
    {
        var result = await mediator.Send(new UpdateRecipeCommand(id, recipeUpdateDto));

        return result.Value;
    }

}
