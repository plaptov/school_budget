using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using SchoolBudget.Entities;

namespace SchoolBudget;

public class IdConverterConvention : IKeyAddedConvention, IForeignKeyAddedConvention
{
    public void ProcessKeyAdded(IConventionKeyBuilder keyBuilder, IConventionContext<IConventionKeyBuilder> context)
    {
        CheckProperties(keyBuilder.Metadata.Properties);
    }

    public void ProcessForeignKeyAdded(IConventionForeignKeyBuilder foreignKeyBuilder, IConventionContext<IConventionForeignKeyBuilder> context)
    {
        CheckProperties(foreignKeyBuilder.Metadata.Properties);
    }

    private static void CheckProperties(IReadOnlyList<IConventionProperty> props)
    {
        if (props.Count > 1)
            return;

        var prop = props[0];
        if (IsIdType(prop.ClrType))
            prop.Builder.HasConversion(MakeConverterType(prop.ClrType));
    }

    private static bool IsIdType(Type type) => type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Id<>);

    private static Type MakeConverterType(Type idType) => idType.GetNestedType(nameof(Id<int>.EfCoreConverter))!;
}
