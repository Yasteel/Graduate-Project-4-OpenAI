namespace OpenAI_Integration.Models.TextEdit
{
    using Newtonsoft.Json;

    public class Choice
    {
        [JsonProperty("text")]
        public string? Text { get; set; }

        [JsonProperty("index")]
        public int Index { get; set; }
    }
}
