using Asp.Versioning;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using OrderFlow.Inventory.Api.Contracts.Requests;
using OrderFlow.Inventory.Api.Contracts.Responses;
using OrderFlow.Inventory.Application.Abstractions.Services;
using OrderFlow.Inventory.Application.Dtos;

namespace OrderFlow.Inventory.Api.Controllers;

[ApiVersion(1)]
[ApiController]
[Route("api/v{v:apiVersion}/stock")]
public class StockItemController(IStockItemService stockItemService, IMapper mapper) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateStockItemRequest request,
        [FromServices] IValidator<CreateStockItemRequest> validator)
    {
        await validator.ValidateAndThrowAsync(request);

        var dto = mapper.Map<CreateStockItemDto>(request);

        var responseDto = await stockItemService.CreateAsync(dto);

        var response = mapper.Map<CreateStockItemResponse>(responseDto);

        return Accepted(response);
    }
}