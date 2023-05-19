namespace OpenAI_Integration.Models
{
    using OpenAI_Integration.Models.ChatCompletion;

    public class LocalStorage
    {
        public string? CacheKey { get; set; }

        public List<Message>? Messages { get; set; }
    }
}
