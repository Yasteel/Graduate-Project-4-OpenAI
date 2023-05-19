using Newtonsoft.Json;

namespace OpenAI_Integration.Models.ImageGeneration
{
	public class ImageData
	{
		[JsonProperty("url")]
        public string? Path { get; set; }
    }
}
