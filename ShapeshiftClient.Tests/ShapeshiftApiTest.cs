using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using O3one.Shapeshift.Models;

namespace O3one.Shapeshift.Tests
{
	[TestFixture]
	public class ShapeshiftApiTest
	{
		[Test]
		public async Task CanGetPairRate()
		{
			var client = new Client();
			var pairRate = await client.GetPairRateAsync(Currency.BitcoinCash, Currency.Bitcoin);
			Assert.IsNotNull(pairRate);
			Assert.AreEqual(Currency.BitcoinCash, pairRate.From);
			Assert.AreEqual(Currency.Bitcoin, pairRate.To);
			Assert.IsTrue(pairRate.Rate > 0);
		}

		[Test]
		public async Task CanGetPairLimit()
		{
			var client = new Client();
			var pairLimit = await client.GetPairLimitAsync(Currency.BitcoinCash, Currency.Bitcoin);
			Assert.IsNotNull(pairLimit);
			Assert.AreEqual(Currency.BitcoinCash, pairLimit.From);
			Assert.AreEqual(Currency.Bitcoin, pairLimit.To);
			Assert.IsTrue(pairLimit.Limit > 0);
		}

		[Test]
		public async Task CanGetMarketPairInfo()
		{
			var client = new Client();
			var pairInfo = await client.GetMarketPairInfoAsync(Currency.BitcoinCash, Currency.Bitcoin);
			Assert.IsNotNull(pairInfo);
			Assert.AreEqual(Currency.BitcoinCash, pairInfo.From);
			Assert.AreEqual(Currency.Bitcoin, pairInfo.To);
			Assert.IsTrue(pairInfo.Limit > 0);
			Assert.IsTrue(pairInfo.Rate > 0);
			Assert.IsTrue(pairInfo.Min >= 0);
			Assert.IsTrue(pairInfo.MinerFee > 0);
		}

		[Test]
		public async Task CanGetMarketInfo()
		{
			var client = new Client();
			var marketInfo = await client.GetMarketInfoAsync();
			Assert.IsNotNull(marketInfo);
			Assert.IsTrue(marketInfo.Length > 0);
			var btc_ltc = marketInfo.First(x => x.From == Currency.Bitcoin && x.To == Currency.Litecoin);
			Assert.IsNotNull(btc_ltc);
			Assert.IsTrue(btc_ltc.Rate > 1);
		}

		[Test]
		public async Task CanGetRecentTransactions()
		{
			var client = new Client();
			var txs = await client.GetRecentTransactionsAsync();
			Assert.IsNotNull(txs);
			Assert.AreEqual(10, txs.Length);
		}

		[Test]
		public async Task CanGetStatusDepositAddress()
		{
			var client = new Client();
			var status = await client.GetStatusDepositAddressAsync("1BWGRZhcYw6kKYMRozNLFjiTbBKAUNdarc");
			Assert.IsNotNull(status);
			Assert.AreEqual(DepositStatus.Completed, status.Status);
			Assert.AreEqual(Currency.Bitcoin, status.From);
			Assert.AreEqual(Currency.Litecoin, status.To);
			Assert.AreEqual(0.9, status.AmountFrom);
			Assert.IsNotNull(status.TransactionId);
			Assert.IsNull(status.Error);
		}

		[Test]
		public async Task CanProcessErrors_1()
		{
			var client = new Client();
			try
			{
				var status = await client.GetStatusDepositAddressAsync("");
				Assert.Fail("");
			}
			catch (ShapeshiftApiException e)
			{
				Assert.AreEqual("This address is NOT a ShapeShift deposit address. Do not send anything to it.", e.Message);
			}
		}

		[Test]
		public async Task CanProcessErrors_2()
		{
			var client = new Client();
			try
			{
				await client.GetPairRateAsync("hjs", "xll");
				Assert.Fail("An exception was expected here");
			}
			catch (ShapeshiftApiException e)
			{
				Assert.AreEqual("Unknown pair", e.Message);
			}
		}


		[Test]
		public async Task CanGetSupportedCoins()
		{
			var client = new Client();
			var coins = await client.GetSupportedCoinsAsync();
			Assert.IsNotNull(coins);
			Assert.IsTrue(coins.Any(x=> x.Name == "Bitcoin"));
		}

		[Test]
		public async Task CanPostNormalTransaction()
		{
			var client = new Client();
			var txResult = await client.PostNormalTransactionAsync(Currency.Bitcoin, Currency.Litecoin, "Lh9f1gQ1BTYpibixCk4QZWxQvVbtRg76Hm", "1BWGRZhcYw6kKYMRozNLFjiTbBKAUNdarc");
			Assert.IsNotNull(txResult);
			Assert.AreEqual(Currency.Bitcoin, txResult.DepositCoin);
			Assert.AreEqual(Currency.Litecoin, txResult.WithdrawalCoin);
			Assert.AreEqual("Lh9f1gQ1BTYpibixCk4QZWxQvVbtRg76Hm", txResult.WithdrawalAddress);
			Assert.IsNotEmpty(txResult.DepositAddress);
		}
	}
}
