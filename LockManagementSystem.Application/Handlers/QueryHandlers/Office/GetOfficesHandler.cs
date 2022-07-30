using LockManagementSystem.Application.Interface;
using LockManagementSystem.Application.Mappings;
using LockManagementSystem.Application.Models.Queries.Office;
using LockManagementSystem.Application.Models.Responses;
using LockManagementSystem.Domain.Entities;

namespace LockManagementSystem.Application.Handlers.QueryHandlers.Office;

public class GetOfficesHandler : IRequestHandler<GetOfficesQuery, ResponseModel<PagedResponse<OfficeResponse>>>
{
    private readonly IReadRepository<OfficeEntity> _readRepository;
    
    public GetOfficesHandler(IReadRepository<OfficeEntity> readRepository)
    {
        _readRepository = readRepository;
    }

    public async Task<ResponseModel<PagedResponse<OfficeResponse>>> Handle(GetOfficesQuery query, CancellationToken cancellationToken)
    {
        var pagedResult = await _readRepository.GetPagedMultiple(query.PageNumber, query.PageSize, p => !p.IsDeprecated,p => p.Name);
        
        return new ResponseModel<PagedResponse<OfficeResponse>>
        {
            Message = "Offices retrieved successfully",
            Data = new PagedResponse<OfficeResponse>
            {
                Count = pagedResult.Count,
                PageNumber = pagedResult.PageNumber,
                PageSize = pagedResult.PageSize,
                TotalPages = pagedResult.TotalPages,
                Data = LockMapper.Mapper.Map<List<OfficeResponse>>(pagedResult.Data)
            }
        };
    }
}