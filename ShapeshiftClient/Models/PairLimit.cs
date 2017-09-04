using Newtonsoft.Json;

namespace O3one.Shapeshift.Models 
{
	public class PairLimit
	{
		public PairLimit(string from, string to)
		{
			From = from;
			To = to;
		}

		public string From { get; }
		public string To { get; }

		[JsonProperty("limit")]
		public decimal Limit { get; internal set; }
	}
}
