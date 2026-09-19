using AutoMapper;
using OrderFlow.Inventory.Api.Contracts.Requests;
using OrderFlow.Inventory.Api.Contracts.Responses;
using OrderFlow.Inventory.Application.Dtos;

namespace OrderFlow.Inventory.Api.Profiles;

public class StockItemProfile : Profile
{
    public StockItemProfile()
    {
        CreateMap<CreateStockItemRequest, CreateStockItemDto>();
        CreateMap<CreateStockItemResponseDto, CreateStockItemResponse>();
    }
}