using LockManagementSystem.Application.Exceptions;
using LockManagementSystem.Application.Handlers.CommandHandlers.Employee;
using LockManagementSystem.Application.Interface;
using LockManagementSystem.Application.Models.Commands.Employee;
using LockManagementSystem.Domain.Entities;

namespace LockManagementSystem.Tests.Application.Handlers.CommandHandlers.Employee;

public class RegisterEmployeeHandlerTest
{
    private readonly Mock<IWriteRepository<EmployeeDetailEntity>> _writeRepositoryMock;
    private readonly Mock<IReadRepository<EmployeeDetailEntity>> _readRepositoryMock;
    private readonly RegisterEmployeeCommand _command;

    public RegisterEmployeeHandlerTest()
    {
        _writeRepositoryMock = new Mock<IWriteRepository<EmployeeDetailEntity>>();
        _readRepositoryMock = new Mock<IReadRepository<EmployeeDetailEntity>>();
        _command = new RegisterEmployeeCommand
        {
            Address = "No 3, Johnsons street",
            Country = "Nigeria",
            Email = "user@clay.com",
            Gender = "Male",
            Nationality = "Nigerian",
            State = "Lagos",
            EmploymentDate = DateTime.Now,
            FirstName = "example",
            LastName = "user",
            DateOfBirth = DateTime.Now,
            PhoneNumber = "23400000001"
        };
    }

    [Fact]
    public async Task RegisterEmployee_NewEmployee_ReturnsSuccess()
    {
        var cancellationToken = new CancellationToken();
        _readRepositoryMock.Setup(r => r.GetByAsync(p => p.Email.ToLower() == _command.Email))
            .ReturnsAsync(() => null);
        _writeRepositoryMock.Setup(r => r.SaveChangesAsync(cancellationToken)).ReturnsAsync(1);

        var handler = new RegisterEmployeeHandler(_writeRepositoryMock.Object, _readRepositoryMock.Object);
        var result = await handler.Handle(_command, cancellationToken);

        result.Data.Should().NotBeNull();
        result.Data.Email.Should().Be(_command.Email);
    }

    [Fact]
    public async Task RegisterEmployee_DuplicateEmail_ThrowsBadRequestException()
    {
        var cancellationToken = new CancellationToken();
        _readRepositoryMock.Setup(r => r.GetByAsync(It.IsAny<Expression<Func<EmployeeDetailEntity,bool>>>()))
            .ReturnsAsync(new EmployeeDetailEntity());
        _writeRepositoryMock.Setup(r => r.SaveChangesAsync(cancellationToken)).ReturnsAsync(1);

        var handler = new RegisterEmployeeHandler(_writeRepositoryMock.Object, _readRepositoryMock.Object);
        var result = async () =>  await handler.Handle(_command, cancellationToken);
        
        await result.Should().ThrowAsync<BadRequestException>()
            .WithMessage($"Email {_command.Email} already exists.");
    }
}