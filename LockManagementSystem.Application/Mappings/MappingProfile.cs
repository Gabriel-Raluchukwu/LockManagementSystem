using AutoMapper;
using LockManagementSystem.Application.Models.Commands.Employee;
using LockManagementSystem.Application.Models.Responses;
using LockManagementSystem.Domain.Entities;

namespace LockManagementSystem.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterEmployeeCommand, EmployeeDetailEntity>();
        CreateMap<EmployeeDetailEntity, EmployeeDetailsResponse>();
        CreateMap<RegisterEmployeeCommand, EmployeeDetailsResponse>();
    }
}