using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SchoolBudget.Entities;

namespace SchoolBudget;

public class IdConverterConvention : IEntityTypeAddedConvention, IEntityTypeBaseTypeChangedConvention, IKeyAddedConvention
{
    private static bool IsIdType(Type type) => type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Id<>);

    private static Type MakeConverterType(Type idType) => idType
        .GetNestedType(nameof(Id<int>.EfCoreConverter))!
        .MakeGenericType(idType.GetGenericArguments()[0]);

    public void ProcessEntityTypeAdded(IConventionEntityTypeBuilder entityTypeBuilder, IConventionContext<IConventionEntityTypeBuilder> context)
    {
        Process(entityTypeBuilder);
    }

    public void ProcessEntityTypeBaseTypeChanged(IConventionEntityTypeBuilder entityTypeBuilder, IConventionEntityType? newBaseType, IConventionEntityType? oldBaseType, IConventionContext<IConventionEntityType> context)
    {
        if ((newBaseType == null || oldBaseType != null) && entityTypeBuilder.Metadata.BaseType == newBaseType)
        {
            Process(entityTypeBuilder);
        }
    }

    private static void Process(IConventionEntityTypeBuilder entityTypeBuilder)
    {
        foreach (var value in entityTypeBuilder.Metadata.ClrType.GetProperties())
        {
            if (IsIdType(value.PropertyType))
                entityTypeBuilder.Property(value)?.HasConverter(MakeConverterType(value.PropertyType));
        }
    }

    public void ProcessKeyAdded(IConventionKeyBuilder keyBuilder, IConventionContext<IConventionKeyBuilder> context)
    {
        var meta = keyBuilder.Metadata;
        if (meta.IsPrimaryKey() && meta.Properties.Count == 1)
            meta.Properties[0].SetValueGenerationStrategy(NpgsqlValueGenerationStrategy.SerialColumn);
    }
}
