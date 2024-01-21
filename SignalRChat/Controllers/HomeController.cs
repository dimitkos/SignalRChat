using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRChat.Hubs;
using SignalRChat.Models;
using System.Diagnostics;

namespace SignalRChat.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHubContext<DeathlyHallowsHub> _deathlyHub;

        public HomeController(ILogger<HomeController> logger, IHubContext<DeathlyHallowsHub> deathlyHub)
        {
            _logger = logger;
            _deathlyHub = deathlyHub;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> DeathlyHallows(string type)
        {
            if (StaticDetail.DealthyHallowRace.ContainsKey(type))
                StaticDetail.DealthyHallowRace[type]++;

            await _deathlyHub.Clients.All.SendAsync("updateDealthyHallowCount",
                StaticDetail.DealthyHallowRace[StaticDetail.Cloak],
                StaticDetail.DealthyHallowRace[StaticDetail.Stone],
                StaticDetail.DealthyHallowRace[StaticDetail.Wand]);

            return Accepted();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
