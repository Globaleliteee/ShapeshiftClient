using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using O3one.Shapeshift.Messages.Converters;
using O3one.Shapeshift.Models;

namespace O3one.Shapeshift.HttpClient
{
	public static class HttpResponseMessageExtensions
	{
		internal static async Task<T> ReadAsAsync<T>(this Task<HttpResponseMessage> task)
		{
			var response = await task;
			return await response.ReadAsAsync<T>(new JsonMediaTypeFormatter());
		}

		internal static async Task<TResult> ReadAsAsync<TResult, TMediaTypeFormatter>(this Task<HttpResponseMessage> task) where TMediaTypeFormatter : MediaTypeFormatter, new()
		{
			var response = await task;
			return await response.ReadAsAsync<TResult>(new TMediaTypeFormatter());
		}

		internal static async Task<T> XReadAsAsync<T>(this HttpResponseMessage response, params MediaTypeFormatter[] formatters)
		{
			if (!response.IsSuccessStatusCode)
			{
				var content = response.Content;
				var mediaTypes = new MediaTypeFormatter[] { new JsonMediaTypeFormatter() };
				var error = await content.ReadAsAsync<Error>(mediaTypes);
				if(!string.IsNullOrEmpty(error.Description))
					throw new ShapeshiftApiException(error, response.StatusCode);
			}
			
			return await response.Content.ReadAsAsync<T>(formatters);
		}

		internal static async Task<T> ReadAsAsync<T>(this HttpResponseMessage response, params MediaTypeFormatter[] formatters)
		{
			var content = await response.Content.ReadAsStringAsync();
			if (content.Contains("\"error\""))
			{
				var jerror = JObject.Parse(content);
				var error = jerror.ToObject<Error>();
				if (!string.IsNullOrEmpty(error.Description))
					throw new ShapeshiftApiException(error, response.StatusCode);
			}

			var ser = JsonSerializer.Create();
			ser.Converters.Add(new PairRateConverter());
			ser.Converters.Add(new SupportedCoinConverter());
			var obj = ser.Deserialize<T>(new JsonTextReader(new System.IO.StringReader(content)));

			return obj;
		}
	}
}
