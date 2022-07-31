using LockManagementSystem.Application.Exceptions;
using LockManagementSystem.Application.Handlers.CommandHandlers.Role;
using LockManagementSystem.Application.Interface;
using LockManagementSystem.Application.Models.Commands.Roles;
using LockManagementSystem.Domain.Entities;

namespace LockManagementSystem.Tests.Application.Handlers.CommandHandlers.Role;

public class UpdateRoleHandlerTest
{
    private readonly Mock<IWriteRepository<RoleEntity>> _roleWriteRepositoryMock;
    private readonly Mock<IReadRepository<RoleEntity>> _roleReadRepositoryMock;
    private readonly UpdateRoleCommand _command;
    private readonly CancellationToken _cancellationToken;
    
    public UpdateRoleHandlerTest()
    {
        _roleWriteRepositoryMock = new Mock<IWriteRepository<RoleEntity>>();
        _roleReadRepositoryMock = new Mock<IReadRepository<RoleEntity>>();
        _command = new UpdateRoleCommand
        {
            Id = Guid.NewGuid(),
            Name = "Manager"
        };
        _cancellationToken = new CancellationToken();
    }
    
    [Fact]
    public async Task UpdateRole_ExistingRole_ReturnsSuccess()
    {
        _roleReadRepositoryMock.Setup(r => r.GetByAsync(It.IsAny<Expression<Func<RoleEntity, bool>>>()))
            .ReturnsAsync(new RoleEntity{Id = _command.Id, Name = "Manager"});
        _roleWriteRepositoryMock.Setup(r => r.SaveChangesAsync(_cancellationToken)).ReturnsAsync(1);

        var handler = new UpdateRoleHandler( _roleWriteRepositoryMock.Object, _roleReadRepositoryMock.Object);
        var result = await handler.Handle(_command, _cancellationToken);

        result.Data.Should().NotBeNull();
    }
    
    [Fact]
    public async Task UpdateRole_InvalidId_ThrowsNotFoundException()
    {
        _roleReadRepositoryMock.Setup(r => r.GetByAsync(It.IsAny<Expression<Func<RoleEntity, bool>>>()))
            .ReturnsAsync(() => null);

        var handler = new UpdateRoleHandler(_roleWriteRepositoryMock.Object, _roleReadRepositoryMock.Object);
        var result = async () =>  await handler.Handle(_command, _cancellationToken);

        await result.Should().ThrowAsync<NotFoundException>()
            .WithMessage("Role not found.");
    }
}