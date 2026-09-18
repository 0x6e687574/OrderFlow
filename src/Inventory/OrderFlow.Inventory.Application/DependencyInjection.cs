using Microsoft.Extensions.DependencyInjection;
using OrderFlow.Inventory.Application.Abstractions.Services;
using OrderFlow.Inventory.Application.Services;

namespace OrderFlow.Inventory.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IStockItemService, StockItemService>();

        return services;
    }
}