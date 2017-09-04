using Newtonsoft.Json;

namespace O3one.Shapeshift.Models
{
	public class NormalTransactionResult
	{
		[JsonProperty("deposit")]
		public string DepositAddress { get; internal set; }
		[JsonProperty("depositType")]
		public string DepositCoin { get; internal set; }
		[JsonProperty("withdrawal")]
		public string WithdrawalAddress { get; internal set; }
		[JsonProperty("withdrawalType")]
		public string WithdrawalCoin { get; internal set; }
	}
}
