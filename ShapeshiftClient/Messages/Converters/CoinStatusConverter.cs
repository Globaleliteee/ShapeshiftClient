using System;
using Newtonsoft.Json;
using O3one.Shapeshift.Models;

namespace O3one.Shapeshift.Messages.Converters
{
	class CoinStatusConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			var type = typeof(CoinStatus);
			return type.IsAssignableFrom(objectType);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.String)
				return null;
			var status = (string) reader.Value;
			if (status == "available")
				return CoinStatus.Available;
			if (status == "unavailable")
				return CoinStatus.Unavailable;

			return -1;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
		}
	}
}
