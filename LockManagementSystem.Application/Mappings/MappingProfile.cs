using AutoMapper;
using LockManagementSystem.Application.Models.Commands.Employee;
using LockManagementSystem.Application.Models.Commands.EventLog;
using LockManagementSystem.Application.Models.Commands.Lock;
using LockManagementSystem.Application.Models.Commands.Office;
using LockManagementSystem.Application.Models.Responses;
using LockManagementSystem.Domain.Entities;

namespace LockManagementSystem.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //Employee
        CreateMap<RegisterEmployeeCommand, EmployeeDetailEntity>();
        CreateMap<EmployeeDetailEntity, EmployeeDetailsResponse>();
        CreateMap<RegisterEmployeeCommand, EmployeeDetailsResponse>();
        
        //Office
        CreateMap<CreateOfficeCommand, OfficeEntity>();
        CreateMap<OfficeEntity, OfficeResponse>();
        CreateMap<OfficeEntity, CreateOfficeResponse>();
        CreateMap<OfficeEntity, UpdateOfficeResponse>();
        
        //Lock
        CreateMap<CreateLockCommand, LockEntity>();
        CreateMap<LockEntity, LockResponse>();
        CreateMap<LockEntity, CreateLockResponse>();
        CreateMap<LockEntity, UpdateLockResponse>();
        
        //Event Log
        CreateMap<CreateEventLogCommand, EventLogEntity>();
        CreateMap<EventLogEntity, EventLogResponse>();
        CreateMap<EventLogEntity, CreateEventLogResponse>();
    }
}