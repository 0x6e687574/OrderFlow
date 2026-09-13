using FluentValidation;
using OrderFlow.Order.Api.Contracts.Requests;

namespace OrderFlow.Order.Api.Validators;

public class CreateOrderValidator : AbstractValidator<CreateOrderRequest>
{
    public CreateOrderValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty();

        RuleFor(x => x.OrderLines)
            .NotEmpty()
            .Must(x => x
                .Select(ol => ol.Sku)
                .Distinct()
                .Count() == x.Count);

        RuleForEach(x => x.OrderLines)
            .ChildRules(line =>
            {
                line.RuleFor(l => l.Sku)
                    .NotEmpty();

                line.RuleFor(l => l.Quantity)
                    .GreaterThan(0);

                line.RuleFor(l => l.UnitPrice)
                    .GreaterThan(0.00m);
            });
    }
}