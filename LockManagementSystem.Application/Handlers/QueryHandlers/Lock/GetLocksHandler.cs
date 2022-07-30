using LockManagementSystem.Application.Interface;
using LockManagementSystem.Application.Mappings;
using LockManagementSystem.Application.Models.Queries.Lock;
using LockManagementSystem.Application.Models.Responses;
using LockManagementSystem.Domain.Entities;

namespace LockManagementSystem.Application.Handlers.QueryHandlers.Lock;

public class GetLocksHandler : IRequestHandler<GetLocksQuery, ResponseModel<PagedResponse<LockResponse>>>
{
    private readonly IReadRepository<LockEntity> _readRepository;
    
    public GetLocksHandler(IReadRepository<LockEntity> readRepository)
    {
        _readRepository = readRepository;
    }
    
    public async Task<ResponseModel<PagedResponse<LockResponse>>> Handle(GetLocksQuery query, CancellationToken cancellationToken)
    {
        var pagedResult = await _readRepository.GetPagedMultiple(query.PageNumber, query.PageSize, 
            p => p.OfficeId == query.OfficeId && !p.IsDeprecated,p => p.Model);
        
        return new ResponseModel<PagedResponse<LockResponse>>
        {
            Message = "Locks retrieved successfully",
            Data = new PagedResponse<LockResponse>
            {
                Count = pagedResult.Count,
                PageNumber = pagedResult.PageNumber,
                PageSize = pagedResult.PageSize,
                TotalPages = pagedResult.TotalPages,
                Data = LockMapper.Mapper.Map<List<LockResponse>>(pagedResult.Data)
            }
        };
    }
}