using Application.Common.Abstractions.CQRS;
using Application.Common.Abstractions.Repositories;
using Application.Common.Dtos;
using Application.Recipes.Queries;
using AutoMapper;
using Domain.Entities;

namespace Application.Recipes.QueryHandlers;

public class GetAllRecipesQueryHandler : IQueryHandler<GetAllRecipesQuery, IEnumerable<RecipeReadDto>>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Recipe> _repository;

    public GetAllRecipesQueryHandler(IMapper mapper, IRepository<Recipe> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }


    //TODO: use result class from ardalis?
    public async Task<IEnumerable<RecipeReadDto>> Handle(GetAllRecipesQuery request, CancellationToken cancellationToken)
    {
        var recipes = _mapper.Map<IEnumerable<RecipeReadDto>>(await _repository.ListAsync(cancellationToken));

        return recipes;
    }
}
