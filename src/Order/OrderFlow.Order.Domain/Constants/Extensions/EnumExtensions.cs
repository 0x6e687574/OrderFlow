using System.ComponentModel;
using System.Reflection;

namespace OrderFlow.Order.Domain.Constants.Extensions;

public static class EnumExtensions
{
    public static string GetDescription(this Enum @enum)
    {
        return @enum.GetType()
                .GetField(nameof(@enum))?
                .GetCustomAttributes<DescriptionAttribute>()?
                .Description
                ?? @enum.ToString();
    }
}
