using LockManagementSystem.Application.Exceptions;
using LockManagementSystem.Application.Handlers.CommandHandlers.EventLog;
using LockManagementSystem.Application.Interface;
using LockManagementSystem.Application.Models.Commands.EventLog;
using LockManagementSystem.Domain.Entities;
using LockManagementSystem.Domain.Enums;

namespace LockManagementSystem.Tests.Application.Handlers.CommandHandlers.EventLog;

public class CreateEventLogHandlerTest
{
    private readonly Mock<IReadRepository<OfficeEntity>> _officeReadRepositoryMock;
    private readonly Mock<IReadRepository<LockEntity>> _lockReadRepositoryMock;
    private readonly Mock<IWriteRepository<EventLogEntity>> _eventLogWriteRepositoryMock;
    private readonly CreateEventLogCommand _command;
    private readonly CancellationToken _cancellationToken;
    
    public CreateEventLogHandlerTest()
    {
        _officeReadRepositoryMock = new Mock<IReadRepository<OfficeEntity>>();
        _lockReadRepositoryMock = new Mock<IReadRepository<LockEntity>>();
        _eventLogWriteRepositoryMock = new Mock<IWriteRepository<EventLogEntity>>();
        _command = new CreateEventLogCommand
        {
            OfficeId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            LockId = Guid.NewGuid(),
            Status = LockEventStatusEnum.Opened,
            Type = LockEventTypeEnum.Access,
            OccurredAt = DateTime.UtcNow
        };
        _cancellationToken = new CancellationToken();
    }
    
    [Fact]
    public async Task CreateEventLog_NewEvent_ReturnsSuccess()
    {
        _officeReadRepositoryMock.Setup(r => r.GetByAsync(It.IsAny<Expression<Func<OfficeEntity, bool>>>()))
            .ReturnsAsync(new OfficeEntity{Id = _command.OfficeId, Name = "Clay office"});
        _lockReadRepositoryMock.Setup(r => r.GetByAsync(It.IsAny<Expression<Func<LockEntity, bool>>>()))
            .ReturnsAsync(new LockEntity{Id = _command.LockId, OfficeId = _command.OfficeId});
        _eventLogWriteRepositoryMock.Setup(r => r.SaveChangesAsync(_cancellationToken)).ReturnsAsync(1);

        var handler = new CreateEventLogHandler(_officeReadRepositoryMock.Object, _lockReadRepositoryMock.Object, _eventLogWriteRepositoryMock.Object);
        var result = await handler.Handle(_command, _cancellationToken);

        result.Data.Should().NotBeNull();
    }
    
    [Fact]
    public async Task CreateEventLog_InvalidOfficeId_ThrowsNotFoundException()
    {
        _officeReadRepositoryMock.Setup(r => r.GetByAsync(It.IsAny<Expression<Func<OfficeEntity, bool>>>()))
            .ReturnsAsync(() => null);

        var handler = new CreateEventLogHandler(_officeReadRepositoryMock.Object, _lockReadRepositoryMock.Object, _eventLogWriteRepositoryMock.Object);
        var result = async () =>  await handler.Handle(_command, _cancellationToken);

        await result.Should().ThrowAsync<NotFoundException>()
            .WithMessage("Office not found.");
    }
    
    [Fact]
    public async Task CreateEventLog_InvalidLockId_ThrowsNotFoundException()
    {
        _officeReadRepositoryMock.Setup(r => r.GetByAsync(It.IsAny<Expression<Func<OfficeEntity, bool>>>()))
            .ReturnsAsync(new OfficeEntity{Id = _command.OfficeId, Name = "Clay office"});
        _lockReadRepositoryMock.Setup(r => r.GetByAsync(It.IsAny<Expression<Func<LockEntity, bool>>>()))
            .ReturnsAsync(() => null);

        var handler = new CreateEventLogHandler(_officeReadRepositoryMock.Object, _lockReadRepositoryMock.Object, _eventLogWriteRepositoryMock.Object);
        var result = async () =>  await handler.Handle(_command, _cancellationToken);

        await result.Should().ThrowAsync<NotFoundException>()
            .WithMessage("Lock not found.");
    }
}