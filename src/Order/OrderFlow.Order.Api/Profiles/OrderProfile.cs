using AutoMapper;
using OrderFlow.Order.Api.Contracts.Requests;
using OrderFlow.Order.Api.Contracts.Responses;
using OrderFlow.Order.Application.Dtos;

namespace OrderFlow.Order.Api.Profiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<CreateOrderRequest, CreateOrderDto>();
        CreateMap<CreateOrderLineRequest, CreateOrderLineDto>();
        CreateMap<CreateOrderResponseDto, CreateOrderResponse>();

        CreateMap<GetByIdResponseDto, GetByIdResponse>();
        CreateMap<GetByIdOrderLineResponseDto, GetByIdOrderLineResponse>();

        CreateMap<GetByCustomerIdResponseDto, GetByCustomerIdResponse>();
    }
}