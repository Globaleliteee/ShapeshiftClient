using Newtonsoft.Json;
using O3one.Shapeshift.Messages.Converters;

namespace O3one.Shapeshift.Models
{
	public enum DepositStatus
	{
		Completed,
		Failed,
		NoDeposit,
		Received
	}

	public class StatusDepositAddress
	{
		[JsonProperty("status")]
		[JsonConverter(typeof(DepositStatusConverter))]
		public DepositStatus Status { get; internal set; }

		[JsonProperty("address")]
		public string Address { get; internal set; }
		
		[JsonProperty("withdraw")]
		public string WithdrawalAddress { get; internal set; }

		[JsonProperty("incomingCoin")]
		public decimal AmountFrom { get; internal set; }

		[JsonProperty("outgoingCoin")]
		public decimal AmountTo { get; internal set; }

		[JsonProperty("incomingType")]
		public string From { get; internal set; }

		[JsonProperty("outgoingType")]
		public string To { get; internal set; }

		[JsonProperty("transaction")]
		public string TransactionId { get; internal set; }

		[JsonProperty("error")]
		public string Error { get; internal set; }
	}
}
