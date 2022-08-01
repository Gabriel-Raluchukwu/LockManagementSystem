using LockManagementSystem.Application.Exceptions;
using LockManagementSystem.Application.Handlers.CommandHandlers.Lock;
using LockManagementSystem.Application.Interface;
using LockManagementSystem.Application.Models.Commands.Lock;
using LockManagementSystem.Domain.Entities;

namespace LockManagementSystem.Tests.Application.Handlers.CommandHandlers.Locks;

public class OpenLockHandlerTest
{
    private readonly Mock<IReadRepository<LockEntity>> _lockReadRepositoryMock;
    private readonly Mock<IReadRepository<EmployeeEntity>> _employeeReadRepositoryMock;
    private readonly Mock<IReadRepository<EmployeeDetailEntity>> _employeeDetailReadRepositoryMock;
    private readonly Mock<IWriteRepository<EventLogEntity>> _eventLogWriteRepositoryMock;
    private readonly OpenLockCommand _command;
    private readonly CancellationToken _cancellationToken; 
    
    public OpenLockHandlerTest()
    {
        _lockReadRepositoryMock = new Mock<IReadRepository<LockEntity>>();
        _employeeReadRepositoryMock = new Mock<IReadRepository<EmployeeEntity>>();
        _employeeDetailReadRepositoryMock = new Mock<IReadRepository<EmployeeDetailEntity>>();
        _eventLogWriteRepositoryMock = new Mock<IWriteRepository<EventLogEntity>>();
        _command = new OpenLockCommand
        {
            EmployeeId = Guid.NewGuid(),
            LockId = Guid.NewGuid(),
        };
        _cancellationToken = new CancellationToken();
    }
    
    [Fact]
    public async Task OpenLock_ValidLock_ProcessesSuccessfully()
    {
        var officeId = Guid.NewGuid();
        _employeeReadRepositoryMock.Setup(r => r.GetByAsync(It.IsAny<Expression<Func<EmployeeEntity, bool>>>()))
            .ReturnsAsync(new EmployeeEntity{Id = _command.EmployeeId});
        _lockReadRepositoryMock.Setup(r => r.GetByAsync(It.IsAny<Expression<Func<LockEntity, bool>>>()))
            .ReturnsAsync(new LockEntity{Id = _command.LockId, OfficeId = officeId});
        _employeeDetailReadRepositoryMock.Setup(r => r.GetByAsync(It.IsAny<Expression<Func<EmployeeDetailEntity, bool>>>()))
            .ReturnsAsync(new EmployeeDetailEntity{Id = _command.EmployeeId, OfficeId = officeId});
        _eventLogWriteRepositoryMock.Setup(r => r.SaveChangesAsync(_cancellationToken)).ReturnsAsync(1);

        var handler = new OpenLockHandler(_lockReadRepositoryMock.Object, _employeeReadRepositoryMock.Object, _employeeDetailReadRepositoryMock.Object, _eventLogWriteRepositoryMock.Object);
        var result = await handler.Handle(_command, _cancellationToken);

        result.Data.Should().NotBeNull();
    }
    
    [Fact]
    public async Task OpenLock_InvalidEmployee_ThrowsNotFoundException()
    {
        _employeeReadRepositoryMock.Setup(r => r.GetByAsync(It.IsAny<Expression<Func<EmployeeEntity, bool>>>()))
            .ReturnsAsync(() => null);
        
        var handler = new OpenLockHandler(_lockReadRepositoryMock.Object, _employeeReadRepositoryMock.Object, _employeeDetailReadRepositoryMock.Object, _eventLogWriteRepositoryMock.Object);
        var result = async () =>  await handler.Handle(_command, _cancellationToken);

        await result.Should().ThrowAsync<NotFoundException>()
            .WithMessage("Employee not found.");
    }
    
    [Fact]
    public async Task OpenLock_InvalidLock_ThrowsNotFoundException()
    {
        _employeeReadRepositoryMock.Setup(r => r.GetByAsync(It.IsAny<Expression<Func<EmployeeEntity, bool>>>()))
            .ReturnsAsync(new EmployeeEntity{Id = _command.EmployeeId});
        _lockReadRepositoryMock.Setup(r => r.GetByAsync(It.IsAny<Expression<Func<LockEntity, bool>>>()))
            .ReturnsAsync(() => null);
        
        var handler = new OpenLockHandler(_lockReadRepositoryMock.Object, _employeeReadRepositoryMock.Object, _employeeDetailReadRepositoryMock.Object, _eventLogWriteRepositoryMock.Object);
        var result = async () =>  await handler.Handle(_command, _cancellationToken);

        await result.Should().ThrowAsync<NotFoundException>()
            .WithMessage("Lock not found.");
    }
    
    [Fact]
    public async Task OpenLock_MismatchingOfficeId_ThrowsBadRequestException()
    {
        _employeeReadRepositoryMock.Setup(r => r.GetByAsync(It.IsAny<Expression<Func<EmployeeEntity, bool>>>()))
            .ReturnsAsync(new EmployeeEntity{Id = _command.EmployeeId});
        _lockReadRepositoryMock.Setup(r => r.GetByAsync(It.IsAny<Expression<Func<LockEntity, bool>>>()))
            .ReturnsAsync(new LockEntity{Id = _command.LockId, OfficeId = Guid.NewGuid()});
        _employeeDetailReadRepositoryMock.Setup(r => r.GetByAsync(It.IsAny<Expression<Func<EmployeeDetailEntity, bool>>>()))
            .ReturnsAsync(new EmployeeDetailEntity{Id = _command.EmployeeId, OfficeId = Guid.NewGuid()});
        
        var handler = new OpenLockHandler(_lockReadRepositoryMock.Object, _employeeReadRepositoryMock.Object, _employeeDetailReadRepositoryMock.Object, _eventLogWriteRepositoryMock.Object);
        var result = async () =>  await handler.Handle(_command, _cancellationToken);

        await result.Should().ThrowAsync<BadRequestException>()
            .WithMessage("Wrong office.");
    }
}