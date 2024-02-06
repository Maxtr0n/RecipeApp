using Application.Common.Dtos;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace RecipeApi.Controllers;

[ApiController]
[Route("[controller]")]
public class RecipesController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public RecipesController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpGet("id")]
    public async Task<ActionResult<RecipeReadDto>> GetRecipeById([FromRoute] Guid id)
    {

    }

    [HttpGet]
    public async Task<ActionResult<List<RecipeReadDto>>> GetRecipes()
    {

    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RecipeReadDto>> CreateRecipe([FromBody] RecipeCreateDto dto)
    {

    }

    [HttpDelete("id")]
    public async Task<ActionResult> DeleteRecipe([FromRoute] Guid id)
    {

    }

    [HttpPut("id")]
    public async Task<ActionResult<RecipeReadDto>> UpdateRecipe([FromRoute] Guid id)
    {

    }

}
