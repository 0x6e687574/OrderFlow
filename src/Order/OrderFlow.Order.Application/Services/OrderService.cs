using OrderFlow.Order.Application.Abstractions.Services;
using OrderFlow.Order.Application.Abstractions.UnitOfWorks;
using OrderFlow.Order.Application.Dtos;
using OrderFlow.Order.Domain.Entities;

namespace OrderFlow.Order.Application.Services;

using Order = Domain.Entities.Order;

public class OrderService(IUnitOfWork unitOfWork) : IOrderService
{
    public async Task CreateAsync(string customerId, IReadOnlyCollection<OrderLineDto> dtos)
    {
        var order = Order.Create(customerId);

        var orderLines = dtos
            .Select(ol =>
                OrderLine.Create(
                    order.Id,
                    ol.Sku,
                    ol.Quantity,
                    ol.UnitPrice))
            .ToList();

        order.AddRange(orderLines);

        await unitOfWork.Orders.AddAsync(order);

        await unitOfWork.SaveChangesAsync();
    }
}