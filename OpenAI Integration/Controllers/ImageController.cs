namespace OpenAI_Integration.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using OpenAI_Integration.Interfaces;

    public class ImageController : Controller
	{
        private readonly IMessageService messageService;
        private readonly ICacheService cache;

        public ImageController
        (
            IMessageService messageService,
            ICacheService cache
        )
        {
            this.messageService = messageService;
            this.cache = cache;
        }
        public IActionResult Index()
		{
            this.messageService.Remove();
            this.cache.Delete("CurrentChat");
            this.cache.Delete("CurrentImage");
            this.cache.Delete("CurrentText");
            return View();
		}
	}
}
