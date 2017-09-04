using System;
using System.Configuration;

namespace O3one.Shapeshift
{
	internal static class Configuration
	{
		public static Uri ApiUrl { 
			get
			{
				var shapeshiftApiUrl = ConfigurationManager.AppSettings["shapeshift-rest-api-baseurl"];
				return new Uri(shapeshiftApiUrl, UriKind.RelativeOrAbsolute);
			}
		}
	}
}