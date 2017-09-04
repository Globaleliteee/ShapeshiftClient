using Newtonsoft.Json;

namespace O3one.Shapeshift.Models 
{
	public class PairRate
	{
		public PairRate(string from, string to)
		{
			From = from;
			To = to;
		}

		public string From { get; }
		public string To { get; }

		[JsonProperty("rate")]
		public decimal Rate { get; internal set; }
	}
}
