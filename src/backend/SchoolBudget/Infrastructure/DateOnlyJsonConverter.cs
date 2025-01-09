using System.Text.Json;
using System.Text.Json.Serialization;

namespace SchoolBudget.Infrastructure;

public class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    private static readonly JsonConverter<DateOnly> _defaultConverter =
        (JsonConverter<DateOnly>)JsonSerializerOptions.Default.GetConverter(typeof(DateOnly));
    private static readonly JsonConverter<DateTime> _dateTimeConverter =
        (JsonConverter<DateTime>)JsonSerializerOptions.Default.GetConverter(typeof(DateTime));

    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var dt = _dateTimeConverter.Read(ref reader, typeToConvert, options);
        return dt.TimeOfDay == TimeSpan.Zero
            ? DateOnly.FromDateTime(dt)
            : DateOnly.FromDateTime(dt.ToUniversalTime());
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options) =>
        _defaultConverter.Write(writer, value, options);
}