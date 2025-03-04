using AutoMapper;
using TalentHub.UserService.Api.Models.Staff;
using TalentHub.UserService.Application.DTO.Staff;

namespace TalentHub.UserService.Api.Mapping;

public class StaffMappingProfile : Profile
{
    public StaffMappingProfile()
    {
        CreateMap<StaffDto, StaffModel>();
        CreateMap<StaffModel, StaffDto>();
        
        CreateMap<CreateStaffModel, CreateStaffDto>();
        CreateMap<CreateStaffDto, CreateStaffModel>();
        
        CreateMap<UpdateStaffModel, UpdateStaffDto>();
        CreateMap<UpdateStaffDto, UpdateStaffModel>();
    }
}