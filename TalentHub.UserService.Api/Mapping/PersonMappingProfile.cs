using AutoMapper;
using TalentHub.UserService.Api.Models.Person;
using TalentHub.UserService.Application.DTO.Person;

namespace TalentHub.UserService.Api.Mapping;

public class PersonMappingProfile : Profile
{
    public PersonMappingProfile()
    {
        CreateMap<PersonDto, PersonModel>();
        CreateMap<PersonModel, PersonDto>();
        
        CreateMap<CreatePersonModel, CreatePersonDto>();
        CreateMap<CreatePersonDto, CreatePersonModel>();
        
        CreateMap<UpdatePersonModel, UpdatePersonDto>();
        CreateMap<UpdatePersonDto, UpdatePersonModel>();
    }
}