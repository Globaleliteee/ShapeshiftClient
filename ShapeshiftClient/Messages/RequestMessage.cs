using System;
using System.Net.Http;

namespace O3one.Shapeshift.Messages
{
	internal class RequestMessage
	{
		public HttpMethod Method { get; set; }
		public Uri RequestUri { get; set; }
		public string Content { get; set; }
	}
}