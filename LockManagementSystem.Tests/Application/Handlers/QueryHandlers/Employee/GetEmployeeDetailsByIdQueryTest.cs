using LockManagementSystem.Application.Exceptions;
using LockManagementSystem.Application.Handlers.QueryHandlers.Employee;
using LockManagementSystem.Application.Interface;
using LockManagementSystem.Application.Models.Queries.Employee;
using LockManagementSystem.Domain.Entities;

namespace LockManagementSystem.Tests.Application.Handlers.QueryHandlers.Employee;

public class GetEmployeeDetailsByIdQueryTest
{
    private readonly Mock<IReadRepository<EmployeeDetailEntity>> _readRepositoryMock;
    private readonly GetEmployeeDetailQuery _query;
    
    public GetEmployeeDetailsByIdQueryTest()
    {
        _readRepositoryMock = new Mock<IReadRepository<EmployeeDetailEntity>>();
        _query = new GetEmployeeDetailQuery
        {
            Id = Guid.NewGuid()
        };
    }
    
    [Fact]
    public async Task GetEmployeeById_ValidId_ReturnsEmployeeDetails()
    {
        var cancellationToken = new CancellationToken();
        _readRepositoryMock.Setup(r => r.GetByAsync(It.IsAny<Expression<Func<EmployeeDetailEntity,bool>>>()))
            .ReturnsAsync(new EmployeeDetailEntity());

        var handler = new GetEmployeeDetailsByIdQuery(_readRepositoryMock.Object);
        var result = await handler.Handle(_query, cancellationToken);
        
        result.Data.Should().NotBeNull();
    }

    [Fact]
    public async Task GetEmployeeById_InvalidId_ThrowsNotFoundException()
    {
        var cancellationToken = new CancellationToken();
        _readRepositoryMock.Setup(r => r.GetByAsync(It.IsAny<Expression<Func<EmployeeDetailEntity,bool>>>()))
            .ReturnsAsync(() => null);

        var handler = new GetEmployeeDetailsByIdQuery(_readRepositoryMock.Object);
        var result = async () =>  await handler.Handle(_query, cancellationToken);


        await result.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Employee details not found.");
    }
}