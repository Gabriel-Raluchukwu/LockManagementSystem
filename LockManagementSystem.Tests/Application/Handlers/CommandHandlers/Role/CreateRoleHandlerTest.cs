using LockManagementSystem.Application.Exceptions;
using LockManagementSystem.Application.Handlers.CommandHandlers.Role;
using LockManagementSystem.Application.Interface;
using LockManagementSystem.Application.Models.Commands.Roles;
using LockManagementSystem.Domain.Entities;

namespace LockManagementSystem.Tests.Application.Handlers.CommandHandlers.Role;

public class CreateRoleHandlerTest
{
    private readonly Mock<IReadRepository<OfficeEntity>> _officeReadRepositoryMock;
    private readonly Mock<IWriteRepository<RoleEntity>> _roleWriteRepositoryMock;
    private readonly Mock<IReadRepository<RoleEntity>> _roleReadRepositoryMock;
    private readonly CreateRoleCommand _command;
    private readonly CancellationToken _cancellationToken;
   
    public CreateRoleHandlerTest()
    {
        _officeReadRepositoryMock = new Mock<IReadRepository<OfficeEntity>>();
        _roleWriteRepositoryMock = new Mock<IWriteRepository<RoleEntity>>();
        _roleReadRepositoryMock = new Mock<IReadRepository<RoleEntity>>();
        _command = new CreateRoleCommand
        {
            OfficeId = Guid.NewGuid(),
            Name = "Manager",
            Description = "Manager role",
        };
        _cancellationToken = new CancellationToken();
    }
    
    [Fact]
    public async Task CreateRole_NewRole_ReturnsSuccess()
    {
        _officeReadRepositoryMock.Setup(r => r.GetByAsync(It.IsAny<Expression<Func<OfficeEntity, bool>>>()))
            .ReturnsAsync(new OfficeEntity { Id = _command.OfficeId });
        _roleReadRepositoryMock.Setup(r => r.GetByAsync(It.IsAny<Expression<Func<RoleEntity, bool>>>()))
            .ReturnsAsync(() => null);
        _roleWriteRepositoryMock.Setup(r => r.SaveChangesAsync(_cancellationToken)).ReturnsAsync(1);

        var handler = new CreateRoleHandler(_officeReadRepositoryMock.Object, _roleWriteRepositoryMock.Object, _roleReadRepositoryMock.Object);
        var result = await handler.Handle(_command, _cancellationToken);

        result.Data.Should().NotBeNull();
    }
    
    [Fact]
    public async Task CreateRole_InvalidOfficeId_ThrowsNotFoundException()
    {
        _officeReadRepositoryMock.Setup(r => r.GetByAsync(It.IsAny<Expression<Func<OfficeEntity, bool>>>()))
            .ReturnsAsync(() => null);

        var handler = new CreateRoleHandler(_officeReadRepositoryMock.Object, _roleWriteRepositoryMock.Object, _roleReadRepositoryMock.Object);
        var result = async () =>  await handler.Handle(_command, _cancellationToken);

        await result.Should().ThrowAsync<NotFoundException>()
            .WithMessage("Office not found.");
    }
    
    [Fact]
    public async Task CreateRole_DuplicateRole_ThrowsBadRequestException()
    {
        _officeReadRepositoryMock.Setup(r => r.GetByAsync(It.IsAny<Expression<Func<OfficeEntity, bool>>>()))
            .ReturnsAsync(new OfficeEntity { Id = _command.OfficeId });
        _roleReadRepositoryMock.Setup(r => r.GetByAsync(It.IsAny<Expression<Func<RoleEntity, bool>>>()))
            .ReturnsAsync(new RoleEntity{Id = Guid.NewGuid(), OfficeId = _command.OfficeId, Name = _command.Name});

        var handler = new CreateRoleHandler(_officeReadRepositoryMock.Object, _roleWriteRepositoryMock.Object, _roleReadRepositoryMock.Object);
        var result = async () =>  await handler.Handle(_command, _cancellationToken);

        await result.Should().ThrowAsync<BadRequestException>()
            .WithMessage("Role already exists.");
    }
}
