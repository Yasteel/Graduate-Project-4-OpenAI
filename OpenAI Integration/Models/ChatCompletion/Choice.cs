namespace OpenAI_Integration.Models.ChatCompletion
{
    using Newtonsoft.Json;

    public class Choice
    {
        [JsonProperty("message")]
        public Message? Message { get; set; }

        [JsonProperty("finish_reason")]
        public string? FinishReason { get; set; }

    }
}
