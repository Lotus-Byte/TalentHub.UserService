using AutoMapper;
using TalentHub.UserService.Application.DTO.Person;
using TalentHub.UserService.Infrastructure.Models.Users;

namespace TalentHub.UserService.Application.Mapping;

public class PersonMappingProfile : Profile
{
    // TODO: consider to add a date on create and update opts.
    public PersonMappingProfile()
    {
        CreateMap<PersonDto, Person>();
        
        CreateMap<Person, PersonDto>();
        
        CreateMap<CreatePersonDto, Person>()
            .ForMember(d => d.UserId, map => map.Ignore())
            .ForMember(d => d.Created, map => map.Ignore())
            .ForMember(d => d.Updated, map => map.Ignore())
            .ForMember(d => d.Deleted, map => map.Ignore());
        
        CreateMap<UpdatePersonDto, Person>()
            .ForMember(d => d.Created, map => map.Ignore())
            .ForMember(d => d.Updated, map => map.Ignore())
            .ForMember(d => d.Deleted, map => map.Ignore());
    }
}