using System;
using System.Globalization;
using Newtonsoft.Json;

namespace O3one.Shapeshift.Messages.Converters
{
	class NumberToStringConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var val = Convert.ToString(value, CultureInfo.InvariantCulture);
			writer.WriteValue(val);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		public override bool CanConvert(Type objectType)
		{
			return typeof(decimal).IsAssignableFrom(objectType);
		}
	}
}
