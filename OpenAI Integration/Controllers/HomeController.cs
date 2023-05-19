
namespace OpenAI_Integration.Controllers
{
	using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using Microsoft.AspNetCore.Mvc;

	using Newtonsoft.Json;

	using OpenAI_Integration.Interfaces;
	using OpenAI_Integration.Models;
	
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
        private readonly ICacheService cache;
        private readonly IMessageService messageService;

        public HomeController
		(
			ILogger<HomeController> logger,
			ICacheService cache,
			IMessageService messageService
		)
		{
			_logger = logger;
            this.cache = cache;
            this.messageService = messageService;
        }

        public IActionResult Index()
        {
			this.messageService.Remove();
			this.cache.Delete("CurrentChat");
            this.cache.Delete("CurrentImage");
            this.cache.Delete("CurrentText");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}