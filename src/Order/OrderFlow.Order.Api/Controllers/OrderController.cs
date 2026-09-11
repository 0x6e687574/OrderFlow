using Asp.Versioning;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using OrderFlow.Order.Api.Contracts.Requests;
using OrderFlow.Order.Application.Abstractions.Services;
using OrderFlow.Order.Application.Dtos;

namespace OrderFlow.Order.Api.Controllers;

[ApiVersion(1)]
[ApiController]
[Route("api/v{v:apiVersion}/orders")]
public class OrderController(IOrderService orderService, IMapper mapper) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateOrderRequest request,
        [FromServices] IValidator<CreateOrderRequest> validator)
    {
        await validator.ValidateAndThrowAsync(request);

        var dto = mapper.Map<OrderDto>(request);

        await orderService.CreateAsync(dto);

        return Ok();
    }
}