namespace OpenAI_Integration.Models.ImageGeneration
{
    using Newtonsoft.Json;

    public class ImageData
	{
		[JsonProperty("url")]
        public string? Path { get; set; }
    }
}
