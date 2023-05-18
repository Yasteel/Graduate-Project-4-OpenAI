namespace OpenAI_Integration.Models
{
	public class ChatRequestContent
	{
        public string? model { get; set; }

		public List<Message> messages { get; set; }
    }
}
