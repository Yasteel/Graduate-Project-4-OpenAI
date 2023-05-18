using Newtonsoft.Json;

namespace OpenAI_Integration.Models
{
	public class Choice
	{
		[JsonProperty("message")]
		public Message? Message { get; set; }

		[JsonProperty("finish_reason")]
		public string? FinishReason { get; set; }

		[JsonProperty("index")]
		public int Index { get; set; }
	}
}
