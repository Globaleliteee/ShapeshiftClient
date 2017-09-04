using System;
using System.Net;
using O3one.Shapeshift.Models;

namespace O3one.Shapeshift
{
	[Serializable]
	public class ShapeshiftApiException : Exception
	{
		public Error Error { get; }
		public HttpStatusCode HttpStatusCode { get; set; }

		public ShapeshiftApiException(Error error, HttpStatusCode httpStatusCode)
			:base(error.Description)
		{
			Error = error;
			HttpStatusCode = httpStatusCode;
		}
	}
}