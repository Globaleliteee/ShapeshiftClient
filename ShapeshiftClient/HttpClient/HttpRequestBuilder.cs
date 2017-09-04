using System.Net.Http;
using System.Text;
using O3one.Shapeshift.Messages;

namespace O3one.Shapeshift.HttpClient
{
	class HttpRequestBuilder
	{
		public HttpRequestMessage Build(RequestMessage message)
		{
			var request = new HttpRequestMessage(message.Method, message.RequestUri);
			if((message.Method == HttpMethod.Post || message.Method == HttpMethod.Put) 
				&& !string.IsNullOrEmpty(message.Content))
			{
				request.Content = new StringContent(message.Content, Encoding.UTF8, "application/json");
			}
			
			return request;
		}
	}
}
