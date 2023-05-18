
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

        public HomeController(ILogger<HomeController> logger, IApiRequestService apiRequestService)
		{
			_logger = logger;
			this.apiRequestService = apiRequestService;
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