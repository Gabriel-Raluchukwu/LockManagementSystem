using LockManagementSystem.Application.Exceptions;
using LockManagementSystem.Application.Handlers.CommandHandlers.Lock;
using LockManagementSystem.Application.Interface;
using LockManagementSystem.Application.Models.Commands.Lock;
using LockManagementSystem.Domain.Entities;

namespace LockManagementSystem.Tests.Application.Handlers.CommandHandlers.Locks;

public class CrateLockHandlerTest
{
    private readonly Mock<IReadRepository<OfficeEntity>> _officeReadRepositoryMock;
    private readonly Mock<IWriteRepository<LockEntity>> _lockWriteRepositoryMock;
    private readonly CreateLockCommand _command;
    private readonly CancellationToken _cancellationToken;

    public CrateLockHandlerTest()
    {
        _officeReadRepositoryMock = new Mock<IReadRepository<OfficeEntity>>();
        _lockWriteRepositoryMock = new Mock<IWriteRepository<LockEntity>>();
        _command = new CreateLockCommand
        {
            OfficeId = Guid.NewGuid(),
            Location = "Second floor",
            Model = "Clay",
            SerialNo = "1277739231",
            DateInstalled = DateTime.UtcNow
        };
        _cancellationToken = new CancellationToken();
    }
    
    [Fact]
    public async Task CreateLock_NewLock_ReturnsSuccess()
    {
        _officeReadRepositoryMock.Setup(r => r.GetByAsync(It.IsAny<Expression<Func<OfficeEntity, bool>>>()))
            .ReturnsAsync(new OfficeEntity{Id = _command.OfficeId, Name = "Clay office"});
        _lockWriteRepositoryMock.Setup(r => r.SaveChangesAsync(_cancellationToken)).ReturnsAsync(1);

        var handler = new CreateLockHandler(_officeReadRepositoryMock.Object, _lockWriteRepositoryMock.Object);
        var result = await handler.Handle(_command, _cancellationToken);

        result.Data.Should().NotBeNull();
    }
    
    [Fact]
    public async Task CreateLock_InvalidOfficeId_ThrowsNotFoundException()
    {
        _officeReadRepositoryMock.Setup(r => r.GetByAsync(It.IsAny<Expression<Func<OfficeEntity, bool>>>()))
            .ReturnsAsync(() => null);

        var handler = new CreateLockHandler(_officeReadRepositoryMock.Object, _lockWriteRepositoryMock.Object);
        var result = async () =>  await handler.Handle(_command, _cancellationToken);

        await result.Should().ThrowAsync<NotFoundException>()
            .WithMessage("Office not found.");
    }
}