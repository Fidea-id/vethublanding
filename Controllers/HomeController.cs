using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VethubLanding.Interfaces;
using VethubLanding.Models;

namespace VethubLanding.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRestAPIService _restAPIService;

        public HomeController(ILogger<HomeController> logger, IRestAPIService restAPIService)
        {
            _logger = logger;
            _restAPIService = restAPIService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("/price")]
        public IActionResult Price()
        {
            return View();
        }

        [Route("/book")]
        public IActionResult Book()
        {
            return View();
        }
        [HttpGet("/GetPrices")]
        public async Task<IEnumerable<SubscriptionResponse>> GetPrices()
        {
            try
            {
                //Get the AuthToken
                var response = await _restAPIService.GetResponse<BaseAPIResponse<IEnumerable<SubscriptionResponse>>>("Subscription");
                return response.Data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}