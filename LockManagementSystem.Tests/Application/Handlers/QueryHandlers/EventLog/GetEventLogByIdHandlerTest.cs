using LockManagementSystem.Application.Exceptions;
using LockManagementSystem.Application.Handlers.QueryHandlers.EventLog;
using LockManagementSystem.Application.Interface;
using LockManagementSystem.Application.Models.Queries.EventLog;
using LockManagementSystem.Domain.Entities;

namespace LockManagementSystem.Tests.Application.Handlers.QueryHandlers.EventLog;

public class GetEventLogByIdHandlerTest
{
    private readonly Mock<IReadRepository<EventLogEntity>> _readRepositoryMock;
    private readonly GetEventLogByIdQuery _query;
    private readonly CancellationToken _cancellationToken;

    public GetEventLogByIdHandlerTest()
    {
        _readRepositoryMock = new Mock<IReadRepository<EventLogEntity>>();
        _query = new GetEventLogByIdQuery
        {
            Id = Guid.NewGuid()
        };
        _cancellationToken = new CancellationToken();
    }
    
    [Fact]
    public async Task GetEventLogById_ValidId_ReturnsLock()
    {
        _readRepositoryMock.Setup(r => r.GetByAsync(It.IsAny<Expression<Func<EventLogEntity, bool>>>()))
            .ReturnsAsync(new EventLogEntity{ Id = _query.Id });

        var handler = new GetEventLogByIdHandler(_readRepositoryMock.Object);
        var result = await handler.Handle(_query, _cancellationToken);
        
        result.Data.Should().NotBeNull();
    }
    
    [Fact]
    public async Task GetEventLogById_InvalidId_ThrowsNotFoundException()
    {
        _readRepositoryMock.Setup(r => r.GetByAsync(It.IsAny<Expression<Func<EventLogEntity, bool>>>()))
            .ReturnsAsync(() => null);

        var handler = new GetEventLogByIdHandler(_readRepositoryMock.Object);
        var result = async () =>  await handler.Handle(_query, _cancellationToken);
        
        await result.Should().ThrowAsync<NotFoundException>()
            .WithMessage("Event log not found.");
    }
}