using Newtonsoft.Json;
using O3one.Shapeshift.Messages.Converters;

namespace O3one.Shapeshift.Models
{
	public enum CoinStatus
	{
		Available,
		Unavailable
	}


	public class SupportedCoin
	{
		[JsonProperty("name")]
		public string Name { get; internal set; }
		[JsonProperty("symbol")]
		public string Symbol { get; internal set; }
		[JsonProperty("image")]
		public string Image { get; internal set; }
		[JsonProperty("status")]
		[JsonConverter(typeof(CoinStatusConverter))]
		public CoinStatus Status { get; internal set; }
	}
}
