using Newtonsoft.Json;

namespace O3one.Shapeshift.Models 
{
	public class Error
	{
		[JsonProperty("error")]
		public string Description { get; set; }
	}
}
