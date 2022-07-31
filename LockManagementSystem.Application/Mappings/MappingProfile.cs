using AutoMapper;
using LockManagementSystem.Application.Models.Commands.Employee;
using LockManagementSystem.Application.Models.Commands.EmployeeRole;
using LockManagementSystem.Application.Models.Commands.EventLog;
using LockManagementSystem.Application.Models.Commands.Lock;
using LockManagementSystem.Application.Models.Commands.LockRole;
using LockManagementSystem.Application.Models.Commands.Office;
using LockManagementSystem.Application.Models.Commands.Roles;
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
        
        //Auth
        CreateMap<AuthResponse, SignUpResponse>().ReverseMap();
        CreateMap<AuthResponse, SignInResponse>().ReverseMap();
        
        //Roles
        CreateMap<CreateRoleCommand, RoleEntity>();
        CreateMap<RoleEntity, RoleResponse>();
        CreateMap<RoleEntity, CreateRoleResponse>();
        CreateMap<RoleEntity, UpdateRoleResponse>();

        //Employee Roles
        CreateMap<AssignEmployeeRoleCommand, EmployeeRoleEntity>();
        CreateMap<EmployeeRoleEntity, EmployeeRoleResponse>();
        CreateMap<EmployeeRoleEntity, AssignEmployeeToRoleResponse>();
        CreateMap<EmployeeRoleEntity, RemoveEmployeeFromRoleResponse>();
        CreateMap<RoleEntity, EmployeeRoleResponse>();
        
        //Lock Roles
        CreateMap<AssignLockRoleCommand, LockRoleEntity>();
        CreateMap<LockRoleEntity, LockRoleResponse>();
        CreateMap<LockRoleEntity, AssignLockToRoleResponse>();
        CreateMap<LockRoleEntity, RemoveLockFromRoleResponse>();
        CreateMap<RoleEntity, LockRoleResponse>();
    }
}