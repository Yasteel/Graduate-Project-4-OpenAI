using Newtonsoft.Json;

namespace OpenAI_Integration.Models.TextEdit
{
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
