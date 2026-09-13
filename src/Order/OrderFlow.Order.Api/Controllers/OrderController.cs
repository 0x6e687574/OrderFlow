using Asp.Versioning;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using OrderFlow.Order.Api.Contracts.Requests;
using OrderFlow.Order.Api.Contracts.Responses;
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

        var dto = mapper.Map<CreateOrderDto>(request);

        var responseDto = await orderService.CreateAsync(dto);

        var response = mapper.Map<CreateOrderResponse>(responseDto);

        return Accepted(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var responseDto = await orderService.GetByIdAsync(id);

        if (responseDto is null)
        {
            return NotFound();
        }

        var response = mapper.Map<GetByIdResponse>(responseDto);

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetById([FromQuery] string customerId)
    {
        var responseDto = await orderService.GetByCustomerIdAsync(customerId);

        if (responseDto is null)
        {
            return NotFound();
        }

        var response = mapper.Map<GetByCustomerIdResponse>(responseDto);

        return Ok(response);
    }
}