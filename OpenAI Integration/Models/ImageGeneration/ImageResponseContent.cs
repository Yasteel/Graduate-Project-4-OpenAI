using Newtonsoft.Json;

namespace OpenAI_Integration.Models.ImageGeneration
{
	public class ImageResponseContent
	{
		[JsonProperty("created")]
        public long Created { get; set; }

		[JsonProperty("data")]
        public List<ImageData> Data { get; set; }
    }
}
