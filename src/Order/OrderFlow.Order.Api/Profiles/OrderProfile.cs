using AutoMapper;
using OrderFlow.Order.Api.Contracts.Requests;
using OrderFlow.Order.Application.Dtos;

namespace OrderFlow.Order.Api.Profiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<CreateOrderRequest, OrderDto>();
        CreateMap<CreateOrderLineRequest, OrderLineDto>();  
    }
}