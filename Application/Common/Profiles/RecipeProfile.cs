using Application.Common.Dtos;
using Application.Common.Extensions;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Profiles;

public class RecipeProfile : Profile
{
    public RecipeProfile()
    {
        CreateMap<Recipe, RecipeReadDto>()
            .ForMember(dest => dest.Ingredients, m => m.MapFrom(src => src.Ingredients.SplitStringToListOfStrings()))
            .ForMember(dest => dest.Images, m => m.MapFrom(src => src.Images.SplitStringToListOfStrings()));
        CreateMap<RecipeCreateDto, Recipe>()
            .ConstructUsing(x => new Recipe(Guid.NewGuid(), x.Title, x.Ingredients.JoinListToString(), x.Description, x.Images.JoinListToString(), x.Author));
    }
}
