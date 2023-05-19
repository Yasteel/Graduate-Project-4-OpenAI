using OpenAI_Integration.Models.ChatCompletion;

namespace OpenAI_Integration.Models
{
    public class LocalStorage
    {
        public string? CacheKey { get; set; }

        public List<Message> Messages { get; set; }
    }
}
