using AutoMapper;
using TalentHub.UserService.Api.Models.Staff;
using TalentHub.UserService.Application.DTO.Staff;

namespace TalentHub.UserService.Api.Mapping;

public class StaffMappingProfile : Profile
{
    public StaffMappingProfile()
    {
        CreateMap<StaffDto, StaffModel>();
        CreateMap<CreateStaffModel, CreateStaffDto>();
        CreateMap<UpdateStaffModel, UpdateStaffDto>();
    }
}