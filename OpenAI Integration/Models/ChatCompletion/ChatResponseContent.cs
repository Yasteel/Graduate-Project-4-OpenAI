namespace OpenAI_Integration.Models.ChatCompletion
{
    public class ChatResponseContent
    {
        public string? model { get; set; }

        public List<Choice>? choices { get; set; }
    }
}
