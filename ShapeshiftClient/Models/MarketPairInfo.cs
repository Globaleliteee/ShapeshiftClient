using Newtonsoft.Json;

namespace O3one.Shapeshift.Models 
{
	public class MarketPairInfo
	{
		public MarketPairInfo(string from, string to)
		{
			From = from;
			To = to;
		}
		public string From { get; }
		public string To { get; }

		[JsonProperty("rate")]
		public decimal Rate { get; internal set; }

		[JsonProperty("limit")]
		public decimal Limit { get; internal set; }

		[JsonProperty("min")]
		public decimal Min { get; internal set; }

		[JsonProperty("minerFee")]
		public decimal MinerFee { get; internal set; }
	}
}
