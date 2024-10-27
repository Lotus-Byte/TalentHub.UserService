using AutoMapper;
using TalentHub.UserService.Api.Models.Employer;
using TalentHub.UserService.Application.DTO.Employer;

namespace TalentHub.UserService.Api.Mapping;

public class EmployerMappingProfile : Profile
{
    public EmployerMappingProfile()
    {
        CreateMap<EmployerDto, EmployerModel>();
        CreateMap<CreateEmployerModel, CreateEmployerDto>();
        CreateMap<UpdateEmployerModel, UpdateEmployerDto>();
    }
}