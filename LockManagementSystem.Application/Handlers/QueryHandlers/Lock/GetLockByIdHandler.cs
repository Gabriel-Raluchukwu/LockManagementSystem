using LockManagementSystem.Application.Exceptions;
using LockManagementSystem.Application.Interface;
using LockManagementSystem.Application.Mappings;
using LockManagementSystem.Application.Models.Queries.Lock;
using LockManagementSystem.Application.Models.Responses;
using LockManagementSystem.Domain.Entities;

namespace LockManagementSystem.Application.Handlers.QueryHandlers.Lock;

public class GetLockByIdHandler : IRequestHandler<GetLockByIdQuery, ResponseModel<LockResponse>>
{
    private readonly IReadRepository<LockEntity> _readRepository;
    
    public GetLockByIdHandler(IReadRepository<LockEntity> readRepository)
    {
        _readRepository = readRepository;
    }
    
    public async Task<ResponseModel<LockResponse>> Handle(GetLockByIdQuery query, CancellationToken cancellationToken)
    {
        var lockEntity = await _readRepository.GetByAsync(p => p.Id == query.Id && !p.IsDeprecated);

        if (lockEntity is null)
        {
            throw new NotFoundException("Lock not found.");
        }

        return new ResponseModel<LockResponse>
        {
            Message = "Lock retrieved successfully",
            Data = LockMapper.Mapper.Map<LockResponse>(lockEntity)
        };
    }
}