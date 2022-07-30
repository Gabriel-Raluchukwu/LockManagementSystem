using LockManagementSystem.Application.Exceptions;
using LockManagementSystem.Application.Handlers.CommandHandlers.Lock;
using LockManagementSystem.Application.Interface;
using LockManagementSystem.Application.Models.Commands.Lock;
using LockManagementSystem.Domain.Entities;

namespace LockManagementSystem.Tests.Application.Handlers.CommandHandlers.Locks;

public class UpdateLockHandlerTest
{
    private readonly Mock<IReadRepository<OfficeEntity>> _officeReadRepositoryMock; 
    private readonly Mock<IWriteRepository<LockEntity>> _lockWriteRepositoryMock;
    private readonly Mock<IReadRepository<LockEntity>> _lockReadRepositoryMock;
    private readonly UpdateLockCommand _command;
    private readonly CancellationToken _cancellationToken;
    
    public UpdateLockHandlerTest()
    {
        _officeReadRepositoryMock = new Mock<IReadRepository<OfficeEntity>>();
        _lockWriteRepositoryMock = new Mock<IWriteRepository<LockEntity>>();
        _lockReadRepositoryMock = new Mock<IReadRepository<LockEntity>>();
        _command = new UpdateLockCommand
        {
            Id = Guid.NewGuid(),
            OfficeId = Guid.NewGuid(),
             DateInstalled = DateTime.UtcNow
        };
        _cancellationToken = new CancellationToken();
    }
    
    [Fact]
    public async Task UpdateLock_ExistingLock_ReturnsSuccess()
    {
        _lockReadRepositoryMock.Setup(r => r.GetByAsync(It.IsAny<Expression<Func<LockEntity,bool>>>()))
            .ReturnsAsync(new LockEntity{ Id = _command.Id });
        _lockWriteRepositoryMock.Setup(r => r.SaveChangesAsync(_cancellationToken)).ReturnsAsync(1);
        _officeReadRepositoryMock.Setup(r => r.GetByAsync(It.IsAny<Expression<Func<OfficeEntity,bool>>>()))
            .ReturnsAsync(new OfficeEntity{ Id = _command.OfficeId });

        var handler = new UpdateLockHandler(_officeReadRepositoryMock.Object, _lockWriteRepositoryMock.Object, _lockReadRepositoryMock.Object);
        var result = await handler.Handle(_command, _cancellationToken);

        result.Data.Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateLock_InvalidLockId_ThrowsNotFoundException()
    {
        _lockReadRepositoryMock.Setup(r => r.GetByAsync(It.IsAny<Expression<Func<LockEntity,bool>>>()))
            .ReturnsAsync(() => null);

        var handler = new UpdateLockHandler(_officeReadRepositoryMock.Object, _lockWriteRepositoryMock.Object, _lockReadRepositoryMock.Object);
        var result = async () =>  await handler.Handle(_command, _cancellationToken);

        await result.Should().ThrowAsync<NotFoundException>()
            .WithMessage("Lock not found.");
    }

    [Fact]
    public async Task UpdateLock_InvalidOfficeId_ThrowsNotFoundException()
    {
        _lockReadRepositoryMock.Setup(r => r.GetByAsync(It.IsAny<Expression<Func<LockEntity,bool>>>()))
            .ReturnsAsync(new LockEntity{ Id = _command.Id });
        _officeReadRepositoryMock.Setup(r => r.GetByAsync(It.IsAny<Expression<Func<OfficeEntity,bool>>>()))
            .ReturnsAsync(() => null);

        var handler = new UpdateLockHandler(_officeReadRepositoryMock.Object, _lockWriteRepositoryMock.Object, _lockReadRepositoryMock.Object);
        var result = async () =>  await handler.Handle(_command, _cancellationToken);

        await result.Should().ThrowAsync<NotFoundException>()
            .WithMessage("Office not found.");
    }
}