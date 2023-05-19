namespace OpenAI_Integration.Models.TextEdit
{
    using Newtonsoft.Json;

    public class TextResponseContent
    {
        [JsonProperty("object")]
        public string? Object { get; set; }

        [JsonProperty("choices")]
        public List<Choice>? Choices { get; set; }

    }
}