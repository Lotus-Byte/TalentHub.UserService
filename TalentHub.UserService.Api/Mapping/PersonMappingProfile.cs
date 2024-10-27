using AutoMapper;
using TalentHub.UserService.Api.Models.Person;
using TalentHub.UserService.Application.DTO.Person;

namespace TalentHub.UserService.Api.Mapping;

public class PersonMappingProfile : Profile
{
    public PersonMappingProfile()
    {
        CreateMap<PersonDto, PersonModel>();
        CreateMap<CreatePersonModel, CreatePersonDto>();
        CreateMap<UpdatePersonModel, UpdatePersonDto>();
    }
}