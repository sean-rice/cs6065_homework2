#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Cs6065_Homework2.Models;
using Cs6065_Homework2.Models.ViewModels;
using Cs6065_Homework2.Services;

namespace Cs6065_Homework2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ScoringService _scoringService;

        public HomeController(ILogger<HomeController> logger,
            ScoringService scoringService)
        {
            _logger = logger;
            _scoringService = scoringService;
        }

        public async Task<IActionResult> Index()
        {
            ScoreboardViewModel? vm = await _scoringService.GetScoreboardAsync(Enumerable.Range(1, 17));
            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
