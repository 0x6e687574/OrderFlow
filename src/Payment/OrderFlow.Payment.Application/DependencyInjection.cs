using Microsoft.Extensions.DependencyInjection;
using OrderFlow.Payment.Application.Abstractions.Services;
using OrderFlow.Payment.Application.Services;

namespace OrderFlow.Payment.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IPaymentService, PaymentService>();

        return services;
    }
}