using AutoMapper;
using TalentHub.UserService.Application.DTO.Employer;
using TalentHub.UserService.Application.Interfaces;
using TalentHub.UserService.Infrastructure.Abstractions;
using TalentHub.UserService.Infrastructure.Models;

namespace TalentHub.UserService.Application.Services;

public class EmployerService : IEmployerService
{
    private readonly IMapper _mapper;
    private readonly IEmployerRepository _repository;
    
    public EmployerService(IEmployerRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<Guid> CreateEmployerAsync(CreateEmployerDto createEmployerDto)
    {
        var employer = _mapper.Map<CreateEmployerDto, Employer>(createEmployerDto); 
        
        await _repository.AddEmployerAsync(employer);
        
        return employer.UserId;
    }

    public async Task<EmployerDto?> GetEmployerByIdAsync(Guid userId)
    {
        var employer = await _repository.GetEmployerByIdAsync(userId);
        
        if (employer == null) return null;
        
        var employerDto = _mapper.Map<Employer, EmployerDto>(employer);
        
        return employerDto;
    }
    
    public async Task<bool> UpdateEmployerAsync(UpdateEmployerDto updateEmployerDto)
    {
        var employer = await _repository.GetEmployerByIdAsync(updateEmployerDto.UserId);
        
        if (employer == null) return false;
        
        employer = _mapper.Map<UpdateEmployerDto, Employer>(updateEmployerDto);
        
        await _repository.UpdateEmployerAsync(employer);
        
        return true;
    }
    
    public async Task<bool> DeleteEmployerAsync(Guid userId)
    {
        return await _repository.DeleteEmployerAsync(userId);
    }
}