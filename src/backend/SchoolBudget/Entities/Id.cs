﻿using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SchoolBudget.Entities;

[JsonConverter(typeof(JsonIdConverterFactory))]
public readonly struct Id<T> : IEquatable<Id<T>>, IComparable<Id<T>>
{
    private readonly long _value;

    public Id(long value)
    {
        this._value = value;
    }

    public override int GetHashCode()
    {
        return _value.GetHashCode();
    }

    public override string? ToString()
    {
        return _value.ToString();
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is Id<T> other && this == other;
    }

    public bool Equals(Id<T> other) => this == other;

    public int CompareTo(Id<T> other) => _value.CompareTo(other._value);

    public static bool operator ==(Id<T> left, Id<T> right) => left._value == right._value;
    public static bool operator !=(Id<T> left, Id<T> right) => left._value != right._value;
    public static bool operator <(Id<T> left, Id<T> right) => left._value < right._value;
    public static bool operator >(Id<T> left, Id<T> right) => left._value > right._value;
    public static bool operator <=(Id<T> left, Id<T> right) => left._value <= right._value;
    public static bool operator >=(Id<T> left, Id<T> right) => left._value >= right._value;

    public class EfCoreConverter : ValueConverter<Id<T>, long>
    {
        public EfCoreConverter()
            : base(
                v => v._value,
                l => new Id<T>(l)
            )
        {

        }
    }

    public class JsonIdConverter : JsonConverter<Id<T>>
    {
        public override Id<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => new(reader.GetInt64());

        public override void Write(Utf8JsonWriter writer, Id<T> value, JsonSerializerOptions options) => writer.WriteNumberValue(value._value);
    }
}

public class JsonIdConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert) =>
        typeToConvert.IsGenericType &&
        typeToConvert.GetGenericTypeDefinition() == typeof(Id<>);

    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        Type elementType = typeToConvert.GetGenericArguments()[0];

        return (JsonConverter)Activator.CreateInstance(
            typeof(Id<>)
                .GetNestedType(nameof(Id<int>.JsonIdConverter))!
                .MakeGenericType(new Type[] { elementType }))!;
    }
}
