namespace OpenAI_Integration.Models
{
	public class ChatResponseContent
	{
		//public string id { get; set; }
		//public string _object { get; set; }
		//public int created { get; set; }

		public string? model { get; set; }
		public Usage? usage { get; set; }
		public List<Choice>? choices { get; set; }


		
	}
}
