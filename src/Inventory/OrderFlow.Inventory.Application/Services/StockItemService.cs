using OrderFlow.Inventory.Application.Abstractions.Services;
using OrderFlow.Inventory.Application.Abstractions.UnitOfWorks;
using OrderFlow.Inventory.Application.Dtos;
using OrderFlow.Inventory.Application.Exceptions;
using OrderFlow.Inventory.Domain.Entities;

namespace OrderFlow.Inventory.Application.Services;

public class StockItemService(IUnitOfWork unitOfWork) : IStockItemService
{
    public async Task<CreateStockItemResponseDto> CreateAsync(CreateStockItemDto dto)
    {
        if (await IsStockItemExisted(dto.Sku))
        {
            throw new DuplicatedStockItemsException();
        }

        var stockItem = StockItem.Create(dto.Sku, dto.Quantity);

        await unitOfWork.StockItems.AddAsync(stockItem);

        await unitOfWork.SaveChangesAsync();

        var response = new CreateStockItemResponseDto
        {
            Sku = stockItem.Sku,
            QuantityOnHand = stockItem.QuantityOnHand,
            QuantityReserved = stockItem.QuantityReserved
        };

        return response;
    }

    public async Task<IEnumerable<GetStockItemResponseDto>> GetAllAsync()
    {
        var stockItems = await unitOfWork.StockItems.GetAllAsync();

        var response = stockItems
            .Select(si => new GetStockItemResponseDto
            {
                Sku = si.Sku,
                QuantityOnHand = si.QuantityOnHand,
                QuantityReserved = si.QuantityReserved,
                Available = si.QuantityOnHand - si.QuantityReserved
            });

        return response;
    }

    public async Task<AdjustStockItemResponseDto?> AdjustAsync(AdjustStockItemDto dto)
    {
        var stockItem = await unitOfWork.StockItems.GetAsync(dto.Sku);

        if (stockItem is null)
        {
            return null;
        }

        if (!CanAdjustStockItem(dto.Quantity, stockItem.QuantityReserved))
        {
            return null;
        }

        stockItem.Adjust(dto.Quantity);

        await unitOfWork.SaveChangesAsync();

        var response = new AdjustStockItemResponseDto
        {
            Sku = stockItem.Sku,
            QuantityOnHand = stockItem.QuantityOnHand,
            QuantityReserved = stockItem.QuantityReserved,
            Available = stockItem.QuantityOnHand - stockItem.QuantityReserved
        };

        return response;
    }

    private Task<bool> IsStockItemExisted(string sku)
        => unitOfWork.StockItems.ExistsAsync(sku);

    private static bool CanAdjustStockItem(int newQuantity, int quantityReserved)
        => newQuantity >= quantityReserved;
}