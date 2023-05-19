namespace OpenAI_Integration.Models.ChatCompletion
{
    public class ChatRequestContent
    {
        public string? model { get; set; }

        public List<Message>? messages { get; set; }
    }
}
