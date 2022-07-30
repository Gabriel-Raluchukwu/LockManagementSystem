using LockManagementSystem.Application.Exceptions;
using LockManagementSystem.Application.Handlers.QueryHandlers.Office;
using LockManagementSystem.Application.Interface;
using LockManagementSystem.Application.Models.Queries.Office;
using LockManagementSystem.Domain.Entities;

namespace LockManagementSystem.Tests.Application.Handlers.QueryHandlers.Office;

public class GetOfficeByIdHandlerTest
{
    private readonly Mock<IReadRepository<OfficeEntity>> _readRepositoryMock;
    private readonly GetOfficeByIdQuery _query;
    private readonly CancellationToken _cancellationToken;
    
    public GetOfficeByIdHandlerTest()
    {
        _readRepositoryMock = new Mock<IReadRepository<OfficeEntity>>();
        _query = new GetOfficeByIdQuery { Id = Guid.NewGuid() };
        _cancellationToken = new CancellationToken();
    }
    
    [Fact]
    public async Task GetOfficeById_ValidId_ReturnsOffice()
    {
        _readRepositoryMock.Setup(r => r.GetByAsync(It.IsAny<Expression<Func<OfficeEntity,bool>>>()))
            .ReturnsAsync(new OfficeEntity{ Id = _query.Id });

        var handler = new GetOfficeByIdHandler(_readRepositoryMock.Object);
        var result = await handler.Handle(_query, _cancellationToken);
        
        result.Data.Should().NotBeNull();
    }

    [Fact]
    public async Task GetOfficeById_InvalidId_ThrowsNotFoundException()
    {
        _readRepositoryMock.Setup(r => r.GetByAsync(It.IsAny<Expression<Func<OfficeEntity,bool>>>()))
            .ReturnsAsync(() => null);

        var handler = new GetOfficeByIdHandler(_readRepositoryMock.Object);
        var result = async () =>  await handler.Handle(_query, _cancellationToken);
        
        await result.Should().ThrowAsync<NotFoundException>()
            .WithMessage("Office not found.");
    }
}