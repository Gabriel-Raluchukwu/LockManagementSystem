using LockManagementSystem.Application.Handlers.CommandHandlers.Office;
using LockManagementSystem.Application.Interface;
using LockManagementSystem.Application.Models.Commands.Office;
using LockManagementSystem.Domain.Entities;

namespace LockManagementSystem.Tests.Application.Handlers.CommandHandlers.Office;

public class CreateOfficeHandlerTest
{
    private readonly Mock<IWriteRepository<OfficeEntity>> _writeRepositoryMock;
    private readonly CreateOfficeCommand _command;
    private readonly CancellationToken _cancellationToken;
    
    public CreateOfficeHandlerTest()
    {
        _writeRepositoryMock = new Mock<IWriteRepository<OfficeEntity>>();
        _command = new CreateOfficeCommand
        {
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
    public async Task CreateOffice_NewOffice_ReturnsSuccess()
    {
        _writeRepositoryMock.Setup(r => r.SaveChangesAsync(_cancellationToken)).ReturnsAsync(1);

        var handler = new CreateOfficeHandler(_writeRepositoryMock.Object);
        var result = await handler.Handle(_command, _cancellationToken);

        result.Data.Should().NotBeNull();
    }
}