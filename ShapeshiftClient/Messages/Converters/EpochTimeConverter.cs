using System;
using Newtonsoft.Json;

namespace O3one.Shapeshift.Messages.Converters
{
	sealed class UnixDateTimeConverter : JsonConverter
	{
		private static DateTime _epoch0 = new DateTime(1970, 1, 1, 0, 0, 0, 0);

		public override bool CanConvert(Type objectType)
		{
			var dateTimeType = typeof(DateTime);
			return dateTimeType.IsAssignableFrom(objectType);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.Float)
				return null;
			var seconds = (double)reader.Value;
			return _epoch0.AddSeconds(Convert.ToDouble(seconds));
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
		}
	}
}
