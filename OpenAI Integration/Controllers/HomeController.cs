
namespace OpenAI_Integration.Controllers
{
	using System.Diagnostics;

	using Microsoft.AspNetCore.Mvc;

	using Newtonsoft.Json;

	using OpenAI_Integration.Interfaces;
	using OpenAI_Integration.Models;
	
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IApiRequestService apiRequestService;
        private readonly ILocalStorageService localStorageService;

        public HomeController(ILogger<HomeController> logger, IApiRequestService apiRequestService, ILocalStorageService localStorageService)
		{
			_logger = logger;
			this.apiRequestService = apiRequestService;
            this.localStorageService = localStorageService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Submit(string text)
        {
            return View("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}