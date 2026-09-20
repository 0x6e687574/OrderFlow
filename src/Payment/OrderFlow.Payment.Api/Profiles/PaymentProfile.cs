using AutoMapper;
using OrderFlow.Payment.Api.Contracts.Responses;
using OrderFlow.Payment.Application.Dtos;

namespace OrderFlow.Payment.Api.Profiles;

public class PaymentProfile : Profile
{
    public PaymentProfile()
    {
        CreateMap<GetPaymentByOrderIdResponseDto, GetPaymentByOrderIdResponse>();
    }
}