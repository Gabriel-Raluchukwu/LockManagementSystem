using LockManagementSystem.Application.Exceptions;
using LockManagementSystem.Application.Interface;
using LockManagementSystem.Application.Mappings;
using LockManagementSystem.Application.Models.Queries.Office;
using LockManagementSystem.Application.Models.Responses;
using LockManagementSystem.Domain.Entities;

namespace LockManagementSystem.Application.Handlers.QueryHandlers.Office;

public class GetOfficeByIdHandler : IRequestHandler<GetOfficeByIdQuery, ResponseModel<OfficeResponse>>
{
    private readonly IReadRepository<OfficeEntity> _readRepository;
    
    public GetOfficeByIdHandler(IReadRepository<OfficeEntity> readRepository)
    {
        _readRepository = readRepository;
    }

    public async Task<ResponseModel<OfficeResponse>> Handle(GetOfficeByIdQuery query, CancellationToken cancellationToken)
    {
        var office = await _readRepository.GetByAsync(p => p.Id == query.Id && !p.IsDeprecated);

        if (office is null)
        {
            throw new NotFoundException("Office not found.");
        }

        return new ResponseModel<OfficeResponse>
        {
            Message = "Office retrieved successfully",
            Data = LockMapper.Mapper.Map<OfficeResponse>(office)
        };
    }
}