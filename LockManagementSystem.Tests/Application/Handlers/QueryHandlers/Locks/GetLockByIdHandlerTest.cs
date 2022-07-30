using LockManagementSystem.Application.Exceptions;
using LockManagementSystem.Application.Handlers.QueryHandlers.Lock;
using LockManagementSystem.Application.Interface;
using LockManagementSystem.Application.Models.Queries.Lock;
using LockManagementSystem.Domain.Entities;

namespace LockManagementSystem.Tests.Application.Handlers.QueryHandlers.Locks;

public class GetLockByIdHandlerTest
{
    private readonly Mock<IReadRepository<LockEntity>> _readRepositoryMock;
    private readonly GetLockByIdQuery _query;
    private readonly CancellationToken _cancellationToken;
    
    public GetLockByIdHandlerTest()
    {
        _readRepositoryMock = new Mock<IReadRepository<LockEntity>>();
        _query = new GetLockByIdQuery
        {
            Id = Guid.NewGuid()
        };
        _cancellationToken = new CancellationToken();
    }
    
    [Fact]
    public async Task GetLockById_ValidId_ReturnsLock()
    {
        _readRepositoryMock.Setup(r => r.GetByAsync(It.IsAny<Expression<Func<LockEntity, bool>>>()))
            .ReturnsAsync(new LockEntity{ Id = _query.Id });

        var handler = new GetLockByIdHandler(_readRepositoryMock.Object);
        var result = await handler.Handle(_query, _cancellationToken);
        
        result.Data.Should().NotBeNull();
    }

    [Fact]
    public async Task GetLockById_InvalidId_ThrowsNotFoundException()
    {
        _readRepositoryMock.Setup(r => r.GetByAsync(It.IsAny<Expression<Func<LockEntity, bool>>>()))
            .ReturnsAsync(() => null);

        var handler = new GetLockByIdHandler(_readRepositoryMock.Object);
        var result = async () =>  await handler.Handle(_query, _cancellationToken);
        
        await result.Should().ThrowAsync<NotFoundException>()
            .WithMessage("Lock not found.");
    }
}