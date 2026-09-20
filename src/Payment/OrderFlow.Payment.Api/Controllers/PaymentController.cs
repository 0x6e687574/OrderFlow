using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrderFlow.Payment.Api.Contracts.Responses;
using OrderFlow.Payment.Application.Abstractions.Services;

namespace OrderFlow.Payment.Api.Controllers;

[ApiVersion(1)]
[ApiController]
[Route("api/v{v:apiVersion}/payments")]
public class PaymentController(IPaymentService paymentService, IMapper mapper) : ControllerBase
{
    [HttpGet("{orderId:guid}")]
    public async Task<IActionResult> GetByOrderId([FromRoute] Guid orderId)
    {
        var responseDto = await paymentService.GetByOrderIdAsync(orderId);

        if (responseDto is null)
        {
            return NotFound();
        }

        var response = mapper.Map<GetPaymentByOrderIdResponse>(responseDto);

        return Ok(response);
    }
}