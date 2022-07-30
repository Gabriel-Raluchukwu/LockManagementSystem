using LockManagementSystem.Application.Exceptions;
using LockManagementSystem.Application.Interface;
using LockManagementSystem.Application.Mappings;
using LockManagementSystem.Application.Models.Responses;
using LockManagementSystem.Domain.Entities;

namespace LockManagementSystem.Application.Handlers.QueryHandlers.Office;

public class GetOfficeByIdHandler : IRequestHandler<Models.Queries.Office.GetOfficeByIdQuery, ResponseModel<OfficeResponse>>
{
    private readonly IReadRepository<OfficeEntity> _readRepository;
    
    public GetOfficeByIdHandler(IReadRepository<OfficeEntity> readRepository)
    {
        _readRepository = readRepository;
    }

    public async Task<ResponseModel<OfficeResponse>> Handle(Models.Queries.Office.GetOfficeByIdQuery request, CancellationToken cancellationToken)
    {
        var office = await _readRepository.GetByAsync(p => p.Id == request.Id);

        if (office == null)
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