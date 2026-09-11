using Microsoft.Extensions.DependencyInjection;
using OrderFlow.Order.Application.Abstractions.Services;
using OrderFlow.Order.Application.Services;

namespace OrderFlow.Order.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IOrderService, OrderService>();

        return services;
    }
}