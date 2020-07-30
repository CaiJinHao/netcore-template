using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Common.JsonConverter
{
    public class JsonDateTimeNullableConverter: JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.String:
                    {
                        if (DateTime.TryParse(reader.GetString(), out DateTime date))
                        {
                            return date;
                        }
                    }
                    break;
                case JsonTokenType.Number:
                    {
                        if (reader.GetInt64() <= 0)
                        {
                            return DateTime.Now;
                        }
                    }
                    break;
            }
            return reader.GetDateTime();
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            if (value < new DateTime(1901, 1, 1))
            {
                writer.WriteStringValue("");
            }
            else
            {
                writer.WriteStringValue(value.ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }
    }
}
