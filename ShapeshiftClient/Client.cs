using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using O3one.Shapeshift.Models;
using O3one.Shapeshift.HttpClient;
using O3one.Shapeshift.Messages;

namespace O3one.Shapeshift
{
	public class Client
	{
		private readonly HttpRequestBuilder _requestBuilder;

		private System.Net.Http.HttpClient CreateClient()
		{
			var client = new System.Net.Http.HttpClient
			{
				BaseAddress = Configuration.ApiUrl
			};
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			return client;
		}

		public Client()
		{
			_requestBuilder = new HttpRequestBuilder();
		}


		private async Task<HttpResponseMessage> SendAsync(RequestMessage message)
		{
			var request = _requestBuilder.Build(message);
			return await CreateClient().SendAsync(request).ConfigureAwait(false);
		}

		public Task<PairRate> GetPairRateAsync(string from, string to)
		{
			var request = new RequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri($"/rate/{from.ToLower()}_{to.ToLower()}", UriKind.Relative)
			};

			return SendAsync(request).ReadAsAsync<PairRate>();
		}

		public Task<PairLimit> GetPairLimitAsync(string from, string to)
		{
			var request = new RequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri($"/limit/{from.ToLower()}_{to.ToLower()}", UriKind.Relative)
			};

			return SendAsync(request).ReadAsAsync<PairLimit>();
		}

		public Task<MarketPairInfo> GetMarketPairInfoAsync(string from, string to)
		{
			var request = new RequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri($"/marketinfo/{from.ToLower()}_{to.ToLower()}", UriKind.Relative)
			};

			return SendAsync(request).ReadAsAsync<MarketPairInfo>();
		}

		public Task<MarketPairInfo[]> GetMarketInfoAsync()
		{
			var request = new RequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri("/marketinfo/", UriKind.Relative)
			};

			return SendAsync(request).ReadAsAsync<MarketPairInfo[]>();
		}

		public Task<RecentTransaction[]> GetRecentTransactionsAsync(int max = 10)
		{
			if (max <= 0 || max > 50)
				throw new ArgumentOutOfRangeException(nameof(max), max, "number has to be from 1 to 50");

			var request = new RequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri($"/recenttx/{max}", UriKind.Relative)
			};

			return SendAsync(request).ReadAsAsync<RecentTransaction[]>();
		}

		public Task<StatusDepositAddress> GetStatusDepositAddressAsync(string address)
		{
			var request = new RequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri($"/txStat/{address}", UriKind.Relative)
			};

			return SendAsync(request).ReadAsAsync<StatusDepositAddress>();
		}

		public Task<SupportedCoin[]> GetSupportedCoinsAsync()
		{
			var request = new RequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri("/getcoins/", UriKind.Relative)
			};

			return SendAsync(request).ReadAsAsync<SupportedCoin[]>();
		}

		public Task<NormalTransactionResult> PostNormalTransactionAsync(string from, string to, string withdrawalAddress, string returnAddress)
		{
			var x = new
			{
				pair = $"{from.ToLower()}_{to.ToLower()}",
				withdrawal = withdrawalAddress,
				returnAddress = returnAddress
			};
			var request = new RequestMessage
			{
				Method = HttpMethod.Post,
				RequestUri = new Uri("/shift/", UriKind.Relative),
				Content = JObject.FromObject(x).ToString()
			};

			return SendAsync(request).ReadAsAsync<NormalTransactionResult>();
		}

	}
}