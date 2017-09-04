using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using O3one.Shapeshift.Models;

namespace O3one.Shapeshift.Messages.Converters
{
	sealed class PairRateConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			var pairRateType = typeof(PairRate);
			var pairLimitType = typeof(PairLimit);
			var pairInfo = typeof(MarketPairInfo);
			return pairRateType.IsAssignableFrom(objectType) 
				|| pairLimitType.IsAssignableFrom(objectType)
				|| pairInfo.IsAssignableFrom(objectType);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
				return null;
			var jObject = JObject.Load(reader);
			var pair = ((string)jObject["pair"]).ToUpper();
			var sep = pair.IndexOf("_", StringComparison.InvariantCulture);
			var from = pair.Substring(0, sep);
			var to = pair.Substring(sep+1);

			var instance = Activator.CreateInstance(objectType, from, to);
			using (var jObjectReader = CopyReaderForObject(reader, jObject))
			{
				serializer.Populate(jObjectReader, instance);
			}
			return instance;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
		}

		public static JsonReader CopyReaderForObject(JsonReader reader, JObject jObject)
		{
			var jObjectReader = jObject.CreateReader();
			jObjectReader.Culture = reader.Culture;
			jObjectReader.DateParseHandling = reader.DateParseHandling;
			jObjectReader.DateTimeZoneHandling = reader.DateTimeZoneHandling;
			jObjectReader.FloatParseHandling = reader.FloatParseHandling;
			jObjectReader.MaxDepth = reader.MaxDepth;
			return jObjectReader;
		}
	}
}