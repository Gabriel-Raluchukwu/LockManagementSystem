using LockManagementSystem.Application.Exceptions;
using LockManagementSystem.Application.Handlers.CommandHandlers.Employee;
using LockManagementSystem.Application.Interface;
using LockManagementSystem.Application.Models.Commands.Employee;
using LockManagementSystem.Domain.Entities;

namespace LockManagementSystem.Tests.Application.Handlers.CommandHandlers.Employee;

public class RegisterEmployeeHandlerTest
{
    private readonly Mock<IReadRepository<OfficeEntity>> _officeReadRepositoryMock;
    private readonly Mock<IWriteRepository<EmployeeDetailEntity>> _employeeDetailWriteRepositoryMock;
    private readonly Mock<IReadRepository<EmployeeDetailEntity>> _employeeDetailReadRepositoryMock;
    private readonly RegisterEmployeeCommand _command;
    private readonly CancellationToken _cancellationToken;

    public RegisterEmployeeHandlerTest()
    {
        _officeReadRepositoryMock = new Mock<IReadRepository<OfficeEntity>>();
        _employeeDetailWriteRepositoryMock = new Mock<IWriteRepository<EmployeeDetailEntity>>();
        _employeeDetailReadRepositoryMock = new Mock<IReadRepository<EmployeeDetailEntity>>();
        _command = new RegisterEmployeeCommand
        {
            Address = "No 3, Johnson's street",
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
        _cancellationToken = new CancellationToken();
    }

    [Fact]
    public async Task RegisterEmployee_NewEmployee_ReturnsSuccess()
    {
        _officeReadRepositoryMock.Setup(r => r.GetByAsync(It.IsAny<Expression<Func<OfficeEntity, bool>>>()))
            .ReturnsAsync(() => new OfficeEntity{Id = _command.OfficeId});
        _employeeDetailReadRepositoryMock.Setup(r => r.GetByAsync(p => p.Email.ToLower() == _command.Email))
            .ReturnsAsync(() => null);
        _employeeDetailWriteRepositoryMock.Setup(r => r.SaveChangesAsync(_cancellationToken)).ReturnsAsync(1);

        var handler = new RegisterEmployeeHandler(_employeeDetailWriteRepositoryMock.Object, _employeeDetailReadRepositoryMock.Object,
            _officeReadRepositoryMock.Object);
        var result = await handler.Handle(_command, _cancellationToken);

        result.Data.Should().NotBeNull();
        result.Data.Email.Should().Be(_command.Email);
    }

    [Fact]
    public async Task RegisterEmployee_InvalidOfficeId_ThrowsNotFoundException()
    {
        _officeReadRepositoryMock.Setup(r => r.GetByAsync(It.IsAny<Expression<Func<OfficeEntity, bool>>>()))
            .ReturnsAsync(() => null);

        var handler = new RegisterEmployeeHandler(_employeeDetailWriteRepositoryMock.Object, _employeeDetailReadRepositoryMock.Object,
            _officeReadRepositoryMock.Object);
        var result = async () =>  await handler.Handle(_command, _cancellationToken);
        
        await result.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Office not found.");
    }
    
    [Fact]
    public async Task RegisterEmployee_DuplicateEmail_ThrowsBadRequestException()
    {
        _officeReadRepositoryMock.Setup(r => r.GetByAsync(It.IsAny<Expression<Func<OfficeEntity, bool>>>()))
            .ReturnsAsync(() => new OfficeEntity{Id = _command.OfficeId});
        _employeeDetailReadRepositoryMock.Setup(r => r.GetByAsync(It.IsAny<Expression<Func<EmployeeDetailEntity,bool>>>()))
            .ReturnsAsync(new EmployeeDetailEntity());
        _employeeDetailWriteRepositoryMock.Setup(r => r.SaveChangesAsync(_cancellationToken)).ReturnsAsync(1);

        var handler = new RegisterEmployeeHandler(_employeeDetailWriteRepositoryMock.Object, _employeeDetailReadRepositoryMock.Object,
            _officeReadRepositoryMock.Object);
        var result = async () =>  await handler.Handle(_command, _cancellationToken);
        
        await result.Should().ThrowAsync<BadRequestException>()
            .WithMessage($"Email {_command.Email} already exists.");
    }
}