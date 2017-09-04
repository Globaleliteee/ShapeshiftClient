using System;
using Newtonsoft.Json;
using O3one.Shapeshift.Models;

namespace O3one.Shapeshift.Messages.Converters
{
	class DepositStatusConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			var type = typeof(DepositStatus);
			return type.IsAssignableFrom(objectType);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.String)
				return null;
			var status = (string) reader.Value;
			if (status == "failed")
				return DepositStatus.Failed;
			if (status == "no_deposits")
				return DepositStatus.NoDeposit;
			if (status == "received")
				return DepositStatus.Received;
			if (status == "complete")
				return DepositStatus.Completed;

			return -1;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
		}
	}
}
