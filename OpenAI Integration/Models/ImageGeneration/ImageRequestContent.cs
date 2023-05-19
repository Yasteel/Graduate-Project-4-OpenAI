namespace OpenAI_Integration.Models.ImageGeneration
{
	using Newtonsoft.Json;

	public class ImageRequestContent
	{
        [JsonProperty("prompt")]
        public string? Prompt { get; set; }

		[JsonProperty("n")]
		public int NumberImages { get; set; }

		[JsonProperty("size")]
		public string? Size { get; set; }
    }
}
