using FluentValidation;
using OrderFlow.Inventory.Api.Contracts.Requests;

namespace OrderFlow.Inventory.Api.Validators;

public class CreateStockItemValidator : AbstractValidator<CreateStockItemRequest>
{
    private const int MaxSkuLength = 50;
    
    public CreateStockItemValidator()
    {
        RuleFor(x => x.Sku)
            .NotEmpty()
            .MaximumLength(MaxSkuLength);

        RuleFor(x => x.Quantity)
            .GreaterThan(0);
    }
}