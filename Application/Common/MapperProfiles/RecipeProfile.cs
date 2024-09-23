using Application.Common.Dtos;
using Application.Common.Extensions;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.MapperProfiles;

public class RecipeProfile : Profile
{
    public RecipeProfile()
    {
        CreateMap<Recipe, RecipeReadDto>()
            .ForMember(dest => dest.Ingredients, m => m.MapFrom(src => src.Ingredients.SplitStrings()))
            .ForMember(dest => dest.Images, m => m.MapFrom(src => src.Images.SplitStrings()));
        CreateMap<RecipeCreateDto, Recipe>()
            .ForMember(dest => dest.Author.Id, m => m.MapFrom(src => src.AuthorId))
            .ForCtorParam("id", opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForCtorParam("ingredients", opt => opt.MapFrom(src => src.Ingredients.JoinStrings()))
            .ForCtorParam("images", opt => opt.MapFrom(src => src.Images.JoinStrings()));
    }
}
