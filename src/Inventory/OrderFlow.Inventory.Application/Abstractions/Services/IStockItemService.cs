using OrderFlow.Inventory.Application.Dtos;

namespace OrderFlow.Inventory.Application.Abstractions.Services;

public interface IStockItemService
{
    public Task<CreateStockItemResponseDto> CreateAsync(CreateStockItemDto dto);
    public Task<IEnumerable<GetStockItemResponseDto>> GetAllAsync();
    public Task<AdjustStockItemResponseDto?> AdjustAsync(AdjustStockItemDto dto);
}