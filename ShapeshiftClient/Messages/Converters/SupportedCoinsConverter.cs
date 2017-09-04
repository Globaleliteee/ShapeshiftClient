using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using O3one.Shapeshift.Models;

namespace O3one.Shapeshift.Messages.Converters
{
	sealed class SupportedCoinConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			var type = typeof(SupportedCoin[]);
			return type.IsAssignableFrom(objectType); 
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.StartObject)
				return null;
			var jObject = JObject.Load(reader);

			var instance = new List<SupportedCoin>();
			foreach (var jcoin in jObject.Children())
			{
				var coin = jcoin.First.ToObject<SupportedCoin>(serializer);
				instance.Add(coin);
			}
			return instance.ToArray();
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
		}
	}
}