using AutoMapper;
using TalentHub.UserService.Application.DTO.Staff;
using TalentHub.UserService.Infrastructure.Models.Users;

namespace TalentHub.UserService.Application.Mapping;

public class StaffMappingProfile : Profile
{
    // TODO: consider to add a date on create and update opts.
    public StaffMappingProfile()
    {
        CreateMap<StaffDto, Staff>();
        
        CreateMap<Staff, StaffDto>()
            .ForMember(d => d.Created, 
                map => map.MapFrom(src => src.Created.DateTime));
        
        CreateMap<CreateStaffDto, Staff>()
            .ForMember(d => d.UserId, map => map.Ignore())
            .ForMember(d => d.Created, map => map.Ignore())
            .ForMember(d => d.Updated, map => map.Ignore())
            .ForMember(d => d.Deleted, map => map.Ignore())
            .ForMember(d => d.UserSettings, map => map.Ignore());
        
        CreateMap<UpdateStaffDto, Staff>()
            .ForMember(d => d.Created, map => map.Ignore())
            .ForMember(d => d.Updated, map => map.Ignore())
            .ForMember(d => d.Deleted, map => map.Ignore());
    }
}