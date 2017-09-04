using System;
using Newtonsoft.Json;
using O3one.Shapeshift.Messages.Converters;

namespace O3one.Shapeshift.Models
{
	public class RecentTransaction
	{
		[JsonProperty("curIn")]
		public string From { get; set; }

		[JsonProperty("curOut")]
		public string To { get; set; }
		
		[JsonProperty("amount")]
		public decimal Amount { get; set; }
		
		[JsonProperty("timestamp")]
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime Timestamp { get; set; }
	}
}