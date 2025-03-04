using AutoMapper;
using TalentHub.UserService.Api.Models.Employer;
using TalentHub.UserService.Application.DTO.Employer;

namespace TalentHub.UserService.Api.Mapping;

public class EmployerMappingProfile : Profile
{
    public EmployerMappingProfile()
    {
        CreateMap<EmployerDto, EmployerModel>();
        CreateMap<EmployerModel, EmployerDto>();
        
        CreateMap<CreateEmployerModel, CreateEmployerDto>();
        CreateMap<CreateEmployerDto, CreateEmployerModel>();
        
        CreateMap<UpdateEmployerModel, UpdateEmployerDto>();
        CreateMap<UpdateEmployerDto, UpdateEmployerModel>();
    }
}