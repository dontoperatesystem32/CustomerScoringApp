using System.Diagnostics.Metrics;
using AutoMapper;
using ScoringSystem_web_api.Dto;
using ScoringSystem_web_api.Models;
using ScoringSystem_web_api.Models.ConditionModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PokemonReviewApp.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<BaseCondition, BaseConditionDto>();
            CreateMap<BaseConditionDto, BaseCondition>();


            //CreateMap<Pokemon, PokemonDto>();
            //CreateMap<Category, CategoryDto>();
            //CreateMap<CategoryDto, Category>();
            //CreateMap<CountryDto, Country>();
            //CreateMap<OwnerDto, Owner>();
            //CreateMap<PokemonDto, Pokemon>();
            //CreateMap<ReviewDto, Review>();
            //CreateMap<ReviewerDto, Reviewer>();
            //CreateMap<Country, CountryDto>();
            //CreateMap<Owner, OwnerDto>();
            //CreateMap<Review, ReviewDto>();
            //CreateMap<Reviewer, ReviewerDto>();

        }
    }
}