using Newtonsoft.Json;

namespace OpenAI_Integration.Models.TextEdit
{
    public class TextResponseContent
    {
        [JsonProperty("object")]
        public string? Object { get; set; }

        [JsonProperty("choices")]
        public List<Choice>? Choices { get; set; }

    }
}

// somthin wong with statement

// fix grammar and spelling errors
