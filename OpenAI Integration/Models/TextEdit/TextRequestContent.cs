namespace OpenAI_Integration.Models.TextEdit
{
    using Newtonsoft.Json;

    public class TextRequestContent
    {
        [JsonProperty("model")]
        public string? Model { get; set; }

        [JsonProperty("input")]
        public string? Input { get; set; }

        [JsonProperty("instruction")]
        public string? Instruction { get; set; }
    }
}
