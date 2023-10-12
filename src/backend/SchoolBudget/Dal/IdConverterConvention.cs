using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using SchoolBudget.Entities;

namespace SchoolBudget;

public class IdConverterConvention : IPropertyAddedConvention
{
    private static bool IsIdType(Type type) => type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Id<>);

    private static Type MakeConverterType(Type idType) => idType.GetNestedType(nameof(Id<int>.EfCoreConverter))!;

    public void ProcessPropertyAdded(IConventionPropertyBuilder propertyBuilder, IConventionContext<IConventionPropertyBuilder> context)
    {
        if (IsIdType(propertyBuilder.Metadata.ClrType))
            propertyBuilder.HasConverter(MakeConverterType(propertyBuilder.Metadata.ClrType));
    }
}
