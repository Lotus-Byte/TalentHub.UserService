using AutoMapper;
using TalentHub.UserService.Application.DTO.Employer;
using TalentHub.UserService.Infrastructure.Models.Users;

namespace TalentHub.UserService.Application.Mapping;

public class EmployerMappingProfile : Profile
{
    // TODO: consider to add a date on create and update opts.
    public EmployerMappingProfile()
    {
        CreateMap<Employer, Employer>();
        
        CreateMap<EmployerDto, Employer>();
        
        CreateMap<Employer, EmployerDto>();
        
        CreateMap<CreateEmployerDto, Employer>()
            .ForMember(d => d.UserId, map => map.Ignore())
            .ForMember(d => d.Created, map => map.Ignore())
            .ForMember(d => d.Updated, map => map.Ignore())
            .ForMember(d => d.Deleted, map => map.Ignore());
        
        CreateMap<UpdateEmployerDto, Employer>()
            .ForMember(d => d.Created, map => map.Ignore())
            .ForMember(d => d.Updated, map => map.Ignore())
            .ForMember(d => d.Deleted, map => map.Ignore());
    }
}