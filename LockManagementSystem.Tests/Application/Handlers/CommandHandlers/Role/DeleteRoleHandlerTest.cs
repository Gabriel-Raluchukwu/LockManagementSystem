using LockManagementSystem.Application.Exceptions;
using LockManagementSystem.Application.Handlers.CommandHandlers.Role;
using LockManagementSystem.Application.Interface;
using LockManagementSystem.Application.Models.Commands.Roles;
using LockManagementSystem.Domain.Entities;

namespace LockManagementSystem.Tests.Application.Handlers.CommandHandlers.Role;

public class DeleteRoleHandlerTest
{
    private readonly Mock<IWriteRepository<RoleEntity>> _roleWriteRepositoryMock;
    private readonly Mock<IReadRepository<RoleEntity>> _roleReadRepositoryMock;
    private readonly DeleteRoleCommand _command;
    private readonly CancellationToken _cancellationToken;

    public DeleteRoleHandlerTest()
    {
        _roleWriteRepositoryMock = new Mock<IWriteRepository<RoleEntity>>();
        _roleReadRepositoryMock = new Mock<IReadRepository<RoleEntity>>();
        _command = new DeleteRoleCommand
        {
           Id = Guid.NewGuid()
        };
        _cancellationToken = new CancellationToken();
    }
    
    [Fact]
    public async Task DeleteRole_ExistingRole_ReturnsSuccess()
    {
        _roleReadRepositoryMock.Setup(r => r.GetByAsync(It.IsAny<Expression<Func<RoleEntity, bool>>>()))
            .ReturnsAsync(new RoleEntity{Id = _command.Id, Name = "Manager"});
        _roleWriteRepositoryMock.Setup(r => r.SaveChangesAsync(_cancellationToken)).ReturnsAsync(1);

        var handler = new DeleteRoleHandler( _roleWriteRepositoryMock.Object, _roleReadRepositoryMock.Object);
        var result = await handler.Handle(_command, _cancellationToken);

        result.Data.Should().Be(true);
    }
    
    [Fact]
    public async Task DeleteRole_InvalidId_ThrowsNotFoundException()
    {
        _roleReadRepositoryMock.Setup(r => r.GetByAsync(It.IsAny<Expression<Func<RoleEntity, bool>>>()))
            .ReturnsAsync(() => null);

        var handler = new DeleteRoleHandler(_roleWriteRepositoryMock.Object, _roleReadRepositoryMock.Object);
        var result = async () =>  await handler.Handle(_command, _cancellationToken);

        await result.Should().ThrowAsync<NotFoundException>()
            .WithMessage("Role not found.");
    }
}