using FluentValidation;
using OrderFlow.Inventory.Api.Contracts.Requests;

namespace OrderFlow.Inventory.Api.Validators;

public class AdjustStockItemValidator : AbstractValidator<AdjustStockItemRequest>
{
    public AdjustStockItemValidator()
    {
        RuleFor(x => x.Quantity)
            .GreaterThan(0);
    }
}