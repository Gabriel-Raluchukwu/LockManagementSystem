using LockManagementSystem.Application.Exceptions;
using LockManagementSystem.Application.Handlers.CommandHandlers.Office;
using LockManagementSystem.Application.Interface;
using LockManagementSystem.Application.Models.Commands.Office;
using LockManagementSystem.Domain.Entities;

namespace LockManagementSystem.Tests.Application.Handlers.CommandHandlers.Office;

public class UpdateOfficeHandlerTest
{
    private readonly Mock<IWriteRepository<OfficeEntity>> _writeRepositoryMock;
    private readonly Mock<IReadRepository<OfficeEntity>> _readRepositoryMock;
    private readonly UpdateOfficeCommand _command;
    private readonly CancellationToken _cancellationToken;
    
    public UpdateOfficeHandlerTest()
    {
        _writeRepositoryMock = new Mock<IWriteRepository<OfficeEntity>>();
        _readRepositoryMock = new Mock<IReadRepository<OfficeEntity>>();
        _command = new UpdateOfficeCommand
        {
            Id = Guid.NewGuid(),
            Name = "",
            Address = "No 3, Johnson's street",
            Country = "Nigeria",
            Description = "GTBank victoria island branch",
            NumberOfDoors = 45,
            NumberOfLocks = 12,
        };
        _cancellationToken = new CancellationToken();
    }
    
    [Fact]
    public async Task UpdateOffice_ExistingOffice_ReturnsSuccess()
    {
        _readRepositoryMock.Setup(r => r.GetByAsync(It.IsAny<Expression<Func<OfficeEntity,bool>>>()))
            .ReturnsAsync(new OfficeEntity{ Id = _command.Id });
        _writeRepositoryMock.Setup(r => r.SaveChangesAsync(_cancellationToken)).ReturnsAsync(1);

        var handler = new UpdateOfficeHandler(_writeRepositoryMock.Object, _readRepositoryMock.Object);
        var result = await handler.Handle(_command, _cancellationToken);

        result.Data.Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateOffice_InvalidId_ThrowsNotFoundException()
    {
        _readRepositoryMock.Setup(r => r.GetByAsync(It.IsAny<Expression<Func<OfficeEntity,bool>>>()))
            .ReturnsAsync(() => null);
        _writeRepositoryMock.Setup(r => r.SaveChangesAsync(_cancellationToken)).ReturnsAsync(1);

        var handler = new UpdateOfficeHandler(_writeRepositoryMock.Object, _readRepositoryMock.Object);
        var result = async () =>  await handler.Handle(_command, _cancellationToken);

        await result.Should().ThrowAsync<NotFoundException>()
            .WithMessage("Office not found.");
    }
}