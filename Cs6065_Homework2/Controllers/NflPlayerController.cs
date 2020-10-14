using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Cs6065_Homework2.Services;

namespace Cs6065_Homework2.Controllers
{
    [Route("Player")]
    public class NflPlayerController : Controller
    {
        private readonly NflPlayerService _nflPlayerService;

        public NflPlayerController(NflPlayerService nflPlayerService)
        {
            _nflPlayerService = nflPlayerService;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [Route(nameof(Quarterback))]
        public async Task<IActionResult> Quarterback()
        {
            var qbs = await _nflPlayerService.GetQuarterbacks();
            return View(qbs); 
        }
        [Route(nameof(Quarterback)+"/json")]
        public async Task<JsonResult> QuarterbackJson()
        {
            var qbs = await _nflPlayerService.GetQuarterbacks();
            return Json(qbs, new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNamingPolicy = null,
                });
        }
    }
}
